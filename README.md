# CARRT Unity Challenge 2024 💻🎮👾
![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)

Welcome to the CARRT Unity Challenge repository! This challenge is designed to test your coding skills as well as your proficiency in collaboration using Git. Follow the instructions below to get started and submit your solutions. 

**To be clear** this challenge will be split between 2 skill sets, working with Unity itself, and working with Web Technologies that interface with Unity, you can choose which skillset you'd prefer to showcase.

## Getting Started

1. **Fork this Repository**: Fork this repository under your account so you can modify it without restriction.
2. **Clone your fork**: Clone your forked repo into your machine so you can make changes
3. **Download Unity 2021.33f1**: Ensure compatibility by using [Unity 2021.33f1](https://unity.com/releases/editor/whats-new/2021.3.33) for development. This version will help avoid compatibility issues.
4. **Open in Unity**: The actual Unity project is UntiyChallenge24, so make sure to open that folder when in Unity Hub.

## Submission Guidelines

To submit your solutions, follow these steps:

**Create a Pull Request**:
   1. Add a pull request in the [Pull Request tab](https://github.com/CARRTLABUSF/UnityChallenge24/pulls) of this repository.
   2. Reference your forked repository in the pull request. (you will have to click on the little hyperlink *compare across forks*)
      
      <img src="https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/3630f18b-e085-45b3-81df-2b962b300624" width=500>

   4. Provide a brief explanation of how your changes solve the challenges
   5. Include links to demo video(s)

**If unable to do pull request:**
- Just email us back a link to your repo, and link(s) to the video(s), but it's worth noting making pull requests is a valuable git skill

**Demo Videos Suggestion**:
   - Use [WeTransfer](https://wetransfer.com/) to upload your video demos and get the links to put in your pull request.

## Challenge Format

As stated before, the challenge is split between 2 categories. Please choose the one you'd like to demonstrate more of your skills about. You don't have to do both categories of the Challenge to submit the PUll Request:

-[ Unity Coding Skills](https://github.com/CARRTLABUSF/UnityChallenge24?tab=readme-ov-file#unity-coding-skills-challenge)
- [Web technology integration skills](https://github.com/CARRTLABUSF/UnityChallenge24/?tab=readme-ov-file#web-technology-integration-skills-challenge)

Each category will have different levels. You don't have to complete all of them to submit the challenge, feel free to go above and beyond at each level.

## Unity Coding Skills Challenge
### Level 1: Moving Laser Hand

**Task**:
- Modify the scripts make the hand move back and forth in the scene
- Next modify `LaserHand.cs` to use Physics Raycasts to destroy the sphere when the hand in front of it.


https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/4217461c-1c0e-4dac-a88d-4c02063bcb42

>**NOTE** your solution doesn't need to show the red laser that's shown in the demo video, that's just for demo purposes

### Level 2: Instantiating Prefabs

- **Task**: Modify the script labeled `GameManager.cs` to:
  - Instantiate prefabs.
  - Ensure prefabs are positioned correctly in the world space

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/b03f1f98-7c6d-4fcb-98cf-611404c68305

### Level 3: Game Object Lives

- **Task**: Modify `LaserHand.cs` and `SphereController.cs`:
  - Prevent the sphere from being destroyed until the 5th hit by the LaserHand raycast. LIke having 5 lives
  - Only `SphereController.cs` must be the one keeping track of the `lives` remaining.
  - Once all the lives are gone, make the Scene Restart after 5 seconds
  - Utilize at least one co-routine for any of the above

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/7bce5954-11fc-4d35-af8b-85a9042821e5

### Extras

These tasks are optional but demonstrate creativity:

- Change the skybox of the scene, materials, or FBX models
- Include a Canvas UI to show the remaining lives
- Make a full game out of this

Feel free to explore these extras to showcase your skills!

## Web technology integration skills Challenge
For this version of the challenge only reference the folder `WebChallengeScripts`. There will be some level of Unity involved but it will also be mostly the testing of Frontend and Backend skills:

### Level 1: Sending UDP packets to a server
- **Task**: Modify `NetworkManager.cs` to:
   - Complete the SendPosition function to get the Position, Velocity and Rotation of the target object (use the helper functions already written)
   - Leverage the `UDP_Data` data structure to package the most recent data (Hint: Research about JSONUtility)
   - Figure out how to use the UDPClient object to send the data to the server
   - To verify proper functionality. Run the `server.exe` that can be found in the `WebChallenge/Server` folder and see if your messages are coming through


https://github.com/user-attachments/assets/4bff86e2-d104-42ab-a742-0608be6cfb4b



### Level 2: Make it pretty
- **Task**: Modify `main.js` and `index.html` inside `WebChallengeScripts/Server/static`:
   - Parse the incoming data coming from the websocket messages and plot it
   - Feel free to use the same graphs from the Demo below but also feel free to be creative
   - The use of the Javascript Canvas is encouraged but not required
   - Make sure your final solution reflects changes in realtime as they are ocurring in the simulation
   - To verify proper functionality. Run the `server.exe` that can be found in the `WebChallenge/Server` folder and then access the HTML via [localhost:8080](http://localhost:8080/)

>**NOTE** if you are not that comfortable with the `Golang` programming language, feel free to stop here but a good tip is to make the best frontend application you can possibly make, go above and beyond to plot that data in a satyisfying way.



https://github.com/user-attachments/assets/d998367c-161f-4710-8b2c-3043122322e6



### Level 3: Send video feed
- **Task**: Create a whole new pipeline to send video feed from Unity:
   - Modify `NetworkManager.cs` to include another Coroutine specifically to send images. Leverage the helper function `GetImageBytes()`. (Hint: Suggest to send small images over UDP)
   - Make sure you send the images to a different server port than the position message
   - Modify `main.go` inside *WebChallengeScripts/server* to create a new UDP listener at the new server port you have defined for image messages
   - Modify `main.go` to enable relaying to the frontend the incoming images. There are multiple ways to achieve this. (Hint: attempt to leverage the already existing Websocket connection with the frontend.)
   - Moidfy `index.html` and `main.js` that you already modified for level 2, but this time add handling such that it can display the incoming images from the server.
   - To verify proper functionality just run `go run main.go` or build it `go build -o server.exe` and then run `./server.exe`

>**NOTE** for this task you must install the Golang programming language in your computer, if you don't have it go [here](https://go.dev/)

https://github.com/user-attachments/assets/33a303c8-0dde-4f9a-9f9c-eef2b932589d


## Foreword

Good luck with the challenge! MTFBWY 🚀

---

CARRT Unity Challenge is created by [L42ARO](https://github.com/L42ARO) and [CARRT](https://github.com/CARRTLABUSF).
