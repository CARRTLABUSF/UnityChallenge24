# CARRT Unity Challenge 2024 💻🎮👾
![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)

# Submission for Jegert Merkaj

Wetransfer link: https://we.tl/t-ALkEwT8IxE

# Explanations

**Level 1**
  - I used the Mathf library to make hand osscilate on a sine wave so the transition betwen back and forth is smoother
  - LaserHand creates and displays Physiscs raycast that detects when it is touching the sphere object to destroy it

**Level 2**
  - Everything from level 1
  - Modified GameManager to instatiate prefab models and make them parents of their respective position gameObjects

**Level 3**
  - Evertyhing from level 2
  - Modified sphereController to keep track of HP/Lives
  - When sphere HP is 0, sphere self destructs
  - LaserHand subtracts 1 hp every new time the raycast touches the sphere

**Level Final/Extra**
  - Added material to sphere to make it look like grumpy cat
  - Whenever Laserhand raycast touches sphere, it shakes to indicate it is taking damage
  - Added CaramellaDansen background music
  - Skipper and Skateboard dance to the beat of CaramellaDansen
  - Added victory screen when sphere is destroyed
  - Changed skybox and Hand color
  

