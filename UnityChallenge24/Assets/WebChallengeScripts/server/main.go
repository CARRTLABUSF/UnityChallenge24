package main

import (
	"fmt"
	"net"
	"net/http"
	"sync"

	"github.com/gorilla/websocket"
)

type UDPData struct {
	PosX   float64 `json:"posX"`
	PosY   float64 `json:"posY"`
	PosZ   float64 `json:"posZ"`
	EulerX float64 `json:"eulerX"`
	EulerY float64 `json:"eulerY"`
	EulerZ float64 `json:"eulerZ"`
	VelX   float64 `json:"velX"`
	VelY   float64 `json:"velY"`
	VelZ   float64 `json:"velZ"`
	VelAbs float64 `json:"velAbs"`
	Time   float64 `json:"time"`
}

var upgrader = websocket.Upgrader{
	CheckOrigin: func(r *http.Request) bool {
		return true
	},
}

var clients = make(map[*websocket.Conn]struct{})
var clientsMutex sync.Mutex

func main() {
	fmt.Println("✅ Server is starting...")

	http.Handle("/", http.FileServer(http.Dir("./static")))
	http.HandleFunc("/ws", handleWebSocket)

	go startPositionUDPServer(":9090")
	go startImageUDPServer(":9091") // 🔹 This function was missing

	fmt.Println("🚀 HTTP server running on http://localhost:8080")
	err := http.ListenAndServe(":8080", nil)
	if err != nil {
		fmt.Println("❌ HTTP server failed:", err)
	}
}

func startPositionUDPServer(port string) {
	udpAddr, err := net.ResolveUDPAddr("udp", port)
	if err != nil {
		fmt.Println("❌ Failed to resolve UDP address:", err)
		return
	}

	conn, err := net.ListenUDP("udp", udpAddr)
	if err != nil {
		fmt.Println("❌ Failed to listen on UDP:", err)
		return
	}
	defer conn.Close()

	fmt.Printf("🛰️  Position UDP server running on %s\n", port)

	buffer := make([]byte, 1024)
	for {
		n, _, err := conn.ReadFromUDP(buffer)
		if err != nil {
			fmt.Println("❌ Error reading UDP:", err)
			continue
		}

		fmt.Println("📡 Received Position Data:", string(buffer[:n]))
		broadcastToWebClients(string(buffer[:n]))
	}
}

func startImageUDPServer(port string) {
	udpAddr, err := net.ResolveUDPAddr("udp", port)
	if err != nil {
		fmt.Println("❌ Failed to resolve Image UDP address:", err)
		return
	}

	conn, err := net.ListenUDP("udp", udpAddr)
	if err != nil {
		fmt.Println("❌ Failed to listen on Image UDP:", err)
		return
	}
	defer conn.Close()

	fmt.Printf("📷 Image UDP server running on %s\n", port)

	buffer := make([]byte, 1024)
	for {
		n, _, err := conn.ReadFromUDP(buffer)
		if err != nil {
			fmt.Println("❌ Error reading Image UDP:", err)
			continue
		}

		fmt.Println("🖼️  Received Image Data:", len(buffer[:n]), "bytes")
		broadcastToWebClients(string(buffer[:n]))
	}
}

func handleWebSocket(w http.ResponseWriter, r *http.Request) {
	conn, err := upgrader.Upgrade(w, r, nil)
	if err != nil {
		fmt.Println("❌ WebSocket error:", err)
		return
	}
	defer conn.Close()

	clientsMutex.Lock()
	clients[conn] = struct{}{}
	fmt.Println("✅ New WebSocket connection")
	clientsMutex.Unlock()
}

func broadcastToWebClients(message string) {
	clientsMutex.Lock()
	defer clientsMutex.Unlock()

	for client := range clients {
		err := client.WriteMessage(websocket.TextMessage, []byte(message))
		if err != nil {
			fmt.Println("❌ WebSocket send error:", err)
			client.Close()
			delete(clients, client)
		} else {
			fmt.Println("📡 WebSocket sent:", message)
		}
	}
}
