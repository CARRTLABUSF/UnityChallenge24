# **Unity Coding Skills Challenge 1-3**

## Task 1

- Updated `HandController.cs` to implement a PingPong movement type using `MoveTowards`, ensuring smooth motion.
- Enhanced `LaserHand.cs` by performing a raycast to detect objects in front of the hand.
    - If the detected object has the "Destroyable" tag and implements the `IDestroyable` interface, it is damaged accordingly, guaranteeing interaction only with destructible entities.

## Task 2

- Added serialize fields to `GameManager.cs` for prefabs to allow assignment of instantiable objects in the Unity Editor.
- Implemented prefab instantiation in `GameManager.cs`

## Task 3

- Created `IDestroyable` interface with `TakeDamage` method for destructible objects.
- Implemented `TakeDamage` in `SphereController`, reducing health by 1 with a 1-second cooldown using a coroutine.
- Made `GameController` a Singleton with a `RestartGame` method, launching a coroutine to restart the scene after 5 seconds.

## Task 1-3 Video

- https://we.tl/t-mt3wo6WdYJ

# Bonus task


## Bonus Video


- https://we.tl/t-7Tfj2nmQQu

## Idea


- Having the Oculus hand as a moving object in the base tasks inspired me to turn it into an actual Oculus hand

## Description


- I reused the base scripts developed in the previous tasks to create a Mixed Reality game. Developed using the Oculus SDK for Meta Quest 3.
- The concept is the following: you get a chance to practice shooting fireballs at falling balloons. But you can’t cast magic without a magic item, so first, you have to put on a wizard hat.
- The goal of your exercise is to prevent any balloons from touching the floor.

## How is this the continuation of the Tasks 1-3?


- Replaced horizontal Oculus Hand movement with real Oculus hand tracking for more natural interaction.
- Fireballs still use Raycast before launching to ensure they accurately reach their target.
- Sphere replaced with Target Balloons, featuring an improved target script based on `SphereController`.
    - Balloons have random HP values (up to 5) with color indicators for health.
    - The Cooldown Coroutine is kept.
    - When destroyed turn black and fly away
- Dynamic prefab instantiation:
    - Fireballs instantiate in the hand when shooting.
    - Wizard hat and note spawn on a random table in the room.
    - Balloons spawn in the air, ensuring they are within reach and not on the ground.
- Expanded GameManager to handle:
    - Scene reload on mission failure or success.
    - UI updates and display.
    - Object spawning based on the actual room layout.

## What is new?


- Gesture Tracking: Fireballs can only be launched when the hand is in the Paper pose (all fingers open, like in Rock, Paper, Scissors).
- Room Tracking: Walls and tables have collisions, and objects are spawned in proper locations based on a room scan.
- Gameplay: The goal is now to destroy all balloons before the touch the floor, and the game can now be repeated without relaunching the app.
- New Unity Features Utilized:
    - Custom event handling scripts for triggers and collisions.
    - Scriptable Object to store object spawner parameters, making it configurable via Unity Events.
    - Animation for the fireball
- Added Grabbable Objects: Hat and Note.
- Hat Trigger on Headset: Hat automatically fits on the head when the headset enters the trigger. *(In recordings, the camera angle makes it seem higher, but it's correctly placed in VR.)*

## Where is the code?


- https://github.com/BulbasDanil/VR_UnityChallenge
- Porting the base project to VR required creating a new project, which is incompatible with the current repository and not logical to pull. Therefore, I uploaded it to a separate repository.

## References


- Hat model [link](https://www.fab.com/listings/d829c36c-9796-46f5-8b33-d50a19a5e38d)
- Fireball model [link](https://sketchfab.com/3d-models/fireball-vfx-911322f594b2480e8c3f9d49ff7619c6)
- Balloon model [link](https://sketchfab.com/3d-models/balloon-2-b6ba1d39d560436289bc6cb39e0c224c)
- Note model [link](https://sketchfab.com/3d-models/paper-e7eeeecef3204e65bde08976a77531ab#download)
