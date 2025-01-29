## Unity Coding Skills Challenge
In this Category you will be building a very simple Unity game that makes use of skills and tools we require for developing VR simulations.

### Part 1: Moving Laser Hand

**Task**:
- Modify the scripts make the hand move back and forth in the scene
- Next modify `LaserHand.cs` to use to destroy the sphere when the hand in front of it. Don't use hard-coded cooridnates to achieve this, look into what Physics tools unity has to offer.


https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/4217461c-1c0e-4dac-a88d-4c02063bcb42

>**NOTE** your solution doesn't need to show the red laser that's shown in the demo video, that's just for demo purposes

### Part 2: Instantiating Prefabs

- **Task**: Modify the script labeled `GameManager.cs` to:
  - Instantiate prefabs.
  - Ensure prefabs are positioned correctly in the world space

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/b03f1f98-7c6d-4fcb-98cf-611404c68305

### Part 3: Game Object Lives

- **Task**: Modify `LaserHand.cs` and `SphereController.cs`:
  - Prevent the sphere from being destroyed until the 5th hit by the LaserHand raycast. LIke having 5 lives
  - Only `SphereController.cs` must be the one keeping track of the `lives` remaining.
  - Once all the lives are gone, make the Scene Restart after 5 seconds
  - Utilize at least one co-routine for any of the above

https://github.com/CARRTLABUSF/UnityChallenge24/assets/89555610/7bce5954-11fc-4d35-af8b-85a9042821e5

### Bonus : Take it beyond

Be Creative and showcase a little more than just this basic game. Here are some ideas:

- Change the skybox of the scene, materials, or FBX models (keep it Safe For Work please)
- Include a Canvas UI to show the remaining lives
- Make a small full game out of this, with menu, points, restart button, etc.