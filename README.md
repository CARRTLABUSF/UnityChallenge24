# CARRT Unity Challenge 2024 💻🎮👾
![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)

Welcome to the CARRT Unity Challenge repository! This challenge is designed to test your Unity coding skills as well as your proficiency in collaboration using Git. Follow the instructions below to get started and submit your solutions.

## Getting Started

1. **Fork this Repository**: Fork this repository under your account so you can modify it without restriction.
2. **Clone your fork**: Clone your forked repo into your machine so you can make changes
3. **Download Unity 2021.33f1**: Ensure compatibility by using Unity 2021.33f1 for development. This version will help avoid compatibility issues.
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

## Challenge Levels

The challenge is divided into three levels. You don't have to complete all of them to submit the challenge, but each level evaluates a unique skill useful for our projects.

### Level 1: Moving Laser Hand

**Task**:
- Modify the script `HandController.cs` to make the hand move back and forth in the scene
- Next modify `LaserHand.cs` to use Physics Raycasts to destroy the sphere when the hand in front of it.


https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/4217461c-1c0e-4dac-a88d-4c02063bcb42

>**NOTE** your solution doesn't need to show the red laser that's shown in the demo video, that's just for demo purposes

### Level 2: Instantiating Prefabs

- **Task**: Modify the script labeled `GameManager.cs` to:
  - Instantiate prefabs that are under resources.
  - Make the prefabs children of `SkipperPos` and `SkateboardPos` respectively in the scene.
  - Ensure prefabs are positioned correctly in the world space

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/b03f1f98-7c6d-4fcb-98cf-611404c68305

### Level 3: Game Object Lives

- **Task**: Modify `LaserHand.cs` and `SphereController.cs`:
  - Prevent the sphere from being destroyed until the 5th hit by the LaserHand raycast. LIke having 5 lives
  - Only `SphereController.cs` must be the one keeping track of the `lives` remaining.

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/7bce5954-11fc-4d35-af8b-85a9042821e5

## Extras

These tasks are optional but demonstrate creativity:

- Change the skybox of the scene.
- Adjust the color of the material of the hand for better visibility.
- Customize the sphere or any other .fbx models to your preference.

Feel free to explore these extras to showcase your skills!

## Foreword

Good luck with the challenge! We look forward to seeing your solutions.

---

CARRT Unity Challenge is created by [L42ARO](https://github.com/L42ARO) and [CARRT](https://github.com/CARRTLABUSF).
