Video Link of explanation:
https://we.tl/t-u2Bk0y4mPf

Unity Coding Skills Challenge

Part 1: Moving Laser Hand

Implemented Hand Movement (HandController.cs)
    1.Used Mathf.PingPong() to move back and forth dynamically on the X-axis.
    2.Updated a LineRenderer to represent the laser.
    3.Added Physics-based Sphere Detection (LaserHand.cs)
    4.Used Physics.Raycast() to detect when the laser beam hits the sphere.
    5.Calls SphereController.TakeDamage() only when the hand is in front of the sphere.

Part 2: Instantiating Prefabs

    1.Instantiates two prefabs (GameManager.cs) dynamically at runtime.
    2.Avoids multiple instantiations by checking if clones already exist.
    3.Ensures correct positioning by using predefined spawn positions.

Part 3: Game Object Lives

    1.Sphere now has 5 lives (SphereController.cs)
    2.The currentLives variable is initialized to 5.
    3.TakeDamage() decreases the lives count on every hit.
    4.Uses a coroutine FlashRed() to give a visual effect when hit.
    5.Implemented Scene Restart (SphereController.cs)
        a.Once the sphere runs out of lives, it enters the DeathSequence coroutine.
        b.The sphere fades out over 1 second and restarts the scene in 5 seconds.
        
        Web technology integration skills Challenge

Part 1: Sending UDP Packets to a Server

    1.Implemented SendPosition() (NetworkManager.cs)
    2.Collects position, velocity, and rotation dynamically.
    3.Uses JsonUtility.ToJson() to serialize data.
    4.Sends data over UDP using UdpClient.
    5.Validated UDP Data Transmission
    6.Verified UDP packets in the command-line logs.
    7.Confirmed successful data reception by the server.exe.

Part 2: Make It Pretty (Frontend Visualization)

    1.Created Graphical Interface (index.html)
    2.Designed grid-based layout for efficient space usage.
    3.Added charts for position, velocity, and rotation.
    4.Included rotation indicators (arrows) using CSS.
    5.Implemented Real-Time Graph Updates (main.js)
    6.Established a WebSocket connection (ws://localhost:8080/ws).
    7.Parsed incoming flight data in the correct API format.
    8.Dynamically updated:
        a.Position Graphs (X-Z, X-Y paths).
        b.Velocity Graphs (X, Y, Z, Absolute Velocity).
        c.Rotation Indicators (X, Y, Z Axis).