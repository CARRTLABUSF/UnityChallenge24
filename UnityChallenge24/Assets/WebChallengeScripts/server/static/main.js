// WebSocket connection
const socket = new WebSocket("ws://localhost:8080/ws");

socket.onopen = () => {
  console.log("WebSocket connection established");
};

socket.onmessage = (event) => {
  if (event.data instanceof Blob) {
    // Hint: Useful distinction
  } 
  else {
    try {
      const data = JSON.parse(event.data);
      console.log(data)
      //Handle incoming websocket messages
      
    } catch (err) {
      console.error("Error parsing position data:", err);
    }
  }
};