// NOTE: For LEVEL 3 suggested to only modify code after line 143
package main

import (
	"encoding/json"
	"fmt"
	"log"
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
		return true // Allow all origins for simplicity
	},
}

var clients = make(map[*websocket.Conn]struct{}) // Use struct{} for zero overhead
var clientsMutex sync.Mutex

func main() {
	// Start the HTTP server for the website
	http.Handle("/", http.FileServer(http.Dir("./static")))
	http.HandleFunc("/ws", handleWebSocket) // WebSocket endpoint
	go func() {
		fmt.Println("HTTP server running on http://localhost:8080")
		if err := http.ListenAndServe(":8080", nil); err != nil {
			log.Fatalf("HTTP server failed: %v", err)
		}
	}()

	// Start the UDP servers
	go startPositionUDPServer(":9090") // Port for position data
	go startImageUDPServer(":9091")

	select {} // Keep the main goroutine alive
}

// UDP server in charge of parsing position data from the simulation
func startPositionUDPServer(port string) {
	udpAddr, err := net.ResolveUDPAddr("udp", port)
	if err != nil {
		log.Fatalf("Failed to resolve position UDP address: %v", err)
	}

	conn, err := net.ListenUDP("udp", udpAddr)
	if err != nil {
		log.Fatalf("Failed to listen on position UDP: %v", err)
	}
	defer conn.Close()

	fmt.Printf("Position UDP server running on port %s\n", port)

	buffer := make([]byte, 1024)
	for {
		n, addr, err := conn.ReadFromUDP(buffer)
		if err != nil {
			log.Printf("Error reading from position UDP: %v", err)
			continue
		}

		message := string(buffer[:n])
		fmt.Printf("Position Data from %s: %s\n", addr.String(), message)

		// Parse and validate JSON
		var udpData UDPData
		if err := json.Unmarshal([]byte(message), &udpData); err != nil {
			log.Printf("Invalid JSON: %v", err)
			continue
		}

		// Broadcast the message to all WebSocket clients
		broadcastToWebClients(message)
	}
}

// Function that handles setting up and maintaining the websocket
func handleWebSocket(w http.ResponseWriter, r *http.Request) {
	conn, err := upgrader.Upgrade(w, r, nil)
	if err != nil {
		log.Printf("Error upgrading WebSocket connection: %v", err)
		return
	}

	// Add the connection to the clients map
	clientsMutex.Lock()
	clients[conn] = struct{}{}
	log.Printf("New WebSocket connection established. Total clients: %d", len(clients))
	clientsMutex.Unlock()

	defer func() {
		// Remove the connection when it closes
		clientsMutex.Lock()
		delete(clients, conn)
		log.Printf("WebSocket connection closed. Total clients: %d", len(clients))
		clientsMutex.Unlock()
		conn.Close()
	}()

	// Keep the connection alive by listening for messages
	for {
		_, msg, err := conn.ReadMessage()
		if err != nil {
			log.Printf("WebSocket read error: %v", err)
			break
		}

		log.Printf("Received message from client: %s", string(msg))
	}
}

func broadcastToWebClients(message string) { // <=== FUNCTION TO SEND WEBSOCKET MESSAGES BACK TO FRONTEND (Only strings though)
	clientsMutex.Lock()
	defer clientsMutex.Unlock()

	for client := range clients {
		err := client.WriteMessage(websocket.TextMessage, []byte(message))
		if err != nil {
			log.Printf("Error sending message to WebSocket client: %v", err)
			client.Close()
			delete(clients, client)
		} else {
			log.Printf("WebSocket sent: %s", message)
		}
	}
}

// ---------------------- MODIFY CODE BELOW -----------------------------------

func startImageUDPServer(port string) {
	//LEVEL 3: COMPLETE ...
}
