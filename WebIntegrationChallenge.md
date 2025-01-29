# Web technology integration skills Challenge

For this Category, a Unity simulation has already been given to you, is a flight simulation for pilots in outer space. You can find it under: `WebChallengeScripts/WebChallengeScene`.

Your tasks will be centered around being able to get data out of this simulation to a server and then onto a Frontend interface, so that ground control can monitor the Pilots training.

> For your convinience everything you will need to modify or run is inside the folder ***Assets/WebChallengeScripts***.

## Part 1: Sending UDP packets to a server
- **Task**: Modify `NetworkManager.cs` to:
   - Complete the SendPosition function to get the Position, Velocity and Rotation of the target object (use the helper functions already written)
   - Leverage the `UDP_Data` data structure to package the most recent data (Hint: Research about JSONUtility)
   - Figure out how to use the UDPClient object to send the data to the server
   - To verify proper functionality. Run the `server.exe` that can be found in the ***WebChallengeScripts/Server*** folder and see if your messages are coming through


https://github.com/user-attachments/assets/4bff86e2-d104-42ab-a742-0608be6cfb4b



## Part 2: Make it pretty
- **Task**: Modify `main.js` and `index.html` inside ***WebChallengeScripts/Server/static***:
   - Parse the incoming data coming from the websocket messages. This is the API format for messages:
      ```js
      {
        "posX": float,   // X-coordinate of the object's position
        "posY": float,   // Y-coordinate of the object's position
        "posZ": float,   // Z-coordinate of the object's position
        "eulerX": float, // X-angle (roll) in Euler coordinates
        "eulerY": float, // Y-angle (pitch) in Euler coordinates
        "eulerZ": float, // Z-angle (yaw) in Euler coordinates
        "velX": float,   // X-component of velocity
        "velY": float,   // Y-component of velocity
        "velZ": float,   // Z-component of velocity
        "velAbs": float, // Absolute velocity magnitude
        "time": float    // Timestamp of the data
      }
      ```
   - Use Javascript, CSS, and HTML to graph the incoming data
     > Feel free to use the same graphs from the Demo below but also feel free to be creative and come up with your own
     > The use of the Javascript Canvas is encouraged but not required
   - Make sure your final solution reflects changes in realtime as they are ocurring in the simulation
   - To verify proper functionality. Run the `server.exe` that can be found in the ***WebChallengeScripts/Server*** folder and then access the HTML via [localhost:8080](http://localhost:8080/)



https://github.com/user-attachments/assets/d998367c-161f-4710-8b2c-3043122322e6


>**NOTE** if you are not that comfortable with working on Backend or the `Golang` programming language, feel free to stop at Part 2 and just make sure to make an outstanding and realtime frontend

## Bonus: Modify backend to handle video feed
- **Task**: Create a whole new pipeline to send video feed from Unity:
   - Modify `NetworkManager.cs` to include another Coroutine specifically to send images. Leverage the helper function `GetImageBytes()`.
     > Note: It's ok to send low resolution and small images
   - Make sure you send the images to a different server port than the position message
   - Modify `main.go` inside *WebChallengeScripts/server* to create a new UDP listener at the new server port you have defined for image messages
   - Modify `main.go` to enable relaying to the frontend the incoming images. There are multiple ways to achieve this
     > Hint: Try to leverage the already existing Websocket connection with the frontend, not the most optimal but it's simple
   - Moidfy `index.html` and `main.js` that you already modified for level 2, but this time add handling such that it can display the incoming images from the server.
   - To verify proper functionality just run `go run main.go` or build it `go build -o server.exe` and then run `./server.exe`

>**NOTE** for this task you must install the Golang programming language in your computer, if you don't have it go [here](https://go.dev/)

https://github.com/user-attachments/assets/33a303c8-0dde-4f9a-9f9c-eef2b932589d

