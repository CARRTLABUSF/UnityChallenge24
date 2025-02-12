# README
## Unity Coding Skills Challenge

### Part 1: Moving Laser Hand   
- Modified LaserHand.cs. The LaserHand script detects objects in 3D space using a ray that follows the position of mouse. 

- It also draws the ray so that can see it in the scene. When the ray hits target object, it keeps track of how many time the target object is hit.
 
- The HandController script makes an object smoothly follow the mouse cursor in 3D space, keeping it positioned at a set depth in front of the camera.

[Download and Watch Unity Coding Challenge Part 1](https://drive.google.com/file/d/1hUDPAu39Exe8brEfWvpU9oZZ1WIjetnQ/view?usp=sharing)


### Part 2: Instantiating Prefabs

- In GameManager.cs, spawn objects by selecting a random prefab from a list and creating it using Unity's Instantiate() function. 

- To make sure objects appear in the right place, calculated a random position around a central spawn point using a mix of random angles and distances. 

- This ensures objects don’t overlap and are evenly spread out. Once an object is spawned, it’s added to a list to track active objects, and after some time, it gets removed to keep the scene balanced. 
- This way, the game continuously spawns and manages objects smoothly as it progresses.

[Download and Watch Unity Coding Challenge Part 2](https://drive.google.com/file/d/1cYDaOkXrFxe4A9dW5Tiub1RbeXS9S9jP/view?usp=sharing)


### Part 3: Game Object Lives
- The LaserHand script tracks how many times a sphere is hit and only reduces its lives after 5 hits by calling DecrementLives() in SphereController. 

- The SphereController script keeps track of the remaining lives and ensures that the sphere is only destroyed when its lives reach zero. 

- Once destroyed, a coroutine waits 5 seconds before restarting the scene, making the game feel smooth and interactive. 

- This setup ensures that all hit tracking happens in LaserHand, while life management and destruction are handled within SphereController, keeping the logic clean and organized. 

[Download and Watch Unity Coding Challenge Part 3](https://drive.google.com/file/d/1ywpdhFTIMc0Gq_Mgz_ATLTpbZ3gU5vsc/view?usp=sharing)

### Bonus : Take it beyond
- Added a new skybox for enhanced visual effects.
- Prefabs are instantiated with a randomized rotation upon spawning.
- Implemented a basic UI with options to resume, play, and quit.


## Web technology integration skills Challenge

### Part 1: Sending UDP packets to a server
- Started with initialising the class variables (such as position, rotation, velocity and time) in the UDP_Data Constructor, which helps to transmit the object over UDP network. 

- Sendposition() function is modified to obtain the object movement data, Initially declared the object variables to get the object movement data such as position, velocity and rotation, after that called the helper functions to get the respective object parameters such as PosX, PosY, PosZ to get X,Y,Z position of the target object and similarly for getting the Velocity and Rotation of target object in X,Y,Z axis.

- Created UDP_Data object to store these values and converted them into JSON format using  JsonUtility.ToJson(obj) and converted the JSON object into bytes to transmit over the UDP network. To verify the functionality, run server.exe to check whether the messages are coming through or not.

[Download and Watch Web technology Challenge Part 1](https://drive.google.com/file/d/1vXwrxTU_i-3vpYN-ZOmlPNe3-ugiifCH/view?usp=sharing)

### Part 2: Make it pretty
- To present the received object movement data on UI, used Chart.js library to provide simple graph representation of real-time data. Firstly, recevied data in JSON format, parsed it and store the values in respective arrays say Velocity={x:[], y:[], z:[], abs=[]}. 

- Then, position and velocity are represented in line graph. createChart() function is called to plot the data points on a line graph where x and y axis parameters are specified. 

- For position, Plotted the graphs between posX-posY and posX-posZ. For velocity, plotted the graphs aganist the time and velX, velY, velZ, velAbs.
 
- Rotation values are represented using triangles to better visualize the angle changes. 

- The drawTriangle() function is responsible for plotting the rotation by drawing and updating the triangle based on the incoming data.

- To ensure a smooth and continuous graph update, stored the incoming data points in an array. 

- Everytime array size reaches 10, removed the oldest data point from satrt of the array and then add the new incoming data at the end of the array. 

- This will create a continuous effect to look like presenting real-time updates.

- In index.html, canvas elements are used for plotting the position, velocity and rotation angles for better visualisation. 

- CSS style tag is used for getting structured layout for the graphs. 

[Download and Watch Web technology Challenge Part 2](https://drive.google.com/file/d/1Gmz9-YD0bXGf18QvymhbULjt25404Z6r/view?usp=sharing)


### Bonus: Modify backend to handle video feed

-  Installed go. public int ImagePort = 9091;. Given 9091 as ImagePort to send the images. 

- Then UDPClient is initialised to handle image transmission and StartCoroutine() to start sending images. 

- Implemented SendImageCoroutine() calls SendImage() to capture and send images. 

- Further, SendImage() function captures an image frame using GetImageBytes(), compresses it, and sends it via UDP to the server on port 9091. 

- This enables real-time video streaming from Unity to the Go server.

- In main.go, implemented the startImageUDPServer(port string) function to listen to imagePort and waits for incoming image data over UDP. 

- Then saved the received image using saveImage() which saves the latest image as "cringe.png".  

- broadcastImageToWebClients() function called to send the latest image to the client for real-time streaming. 

- Modified index.html to include the image tag for displaying real-time streaming image data.

[Download and Watch Web technology Challenge Part 3](https://drive.google.com/file/d/1PlBp4nGj1owWbAuVb7n4FwbYM_HYB-Mr/view?usp=sharing)

