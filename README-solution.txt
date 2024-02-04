Level 1:
Task:
* Modify the script HandController.cs to make the hand move back and forth in the scene
	=> Keep track of the starting positions and then run the logic to move the hand in the Update() function. With the help of `Mathf.PingPong()`, I am able to move the hand between the starting position and the specified distance (3f). Then update the position of the hand to the new position.

* Next modify LaserHand.cs to use Physics Raycasts to destroy the sphere when the hand in front of it.
	=> Using Physics.Raycast, we can identify all GameObjects that are in the path of the hand when a ray is casted forward. When a GameObject with the tag “Destroyable” is detected, destroy the GameObject.



Level 2: Instantiating Prefabs
* Task: Modify the script labeled GameManager.cs to:
    * Instantiate prefabs that are under resources.
    * Make the prefabs children of SkipperPos and SkateboardPos respectively in the scene.
	=> Load the two models with the help of `Resources.Load()` function which can access files in the ‘Resources’ folder. Once the 2 models are loaded in the script, we can instantiate the models as child gameObjects under the 2 publicly accessible GameObjects (skipperPosition and skateboardPosition)
    * Ensure prefabs are positioned correctly in the world space
	=> Reset the local position to ensure that the gameObjects are properly positioned.


Level 3: Game Object Lives
* Task: Modify LaserHand.cs and SphereController.cs:
    * Prevent the sphere from being destroyed until the 5th hit by the LaserHand raycast. LIke having 5 lives
	=> SphereController.cs has 2 public functions that will allow us to reduce and retrieve the lives count. In LaserHand.cs, we need to ensure that the raycast isn’t re-hitting the same object. To keep track of the same object, I update a variable called isHittingGameObject which is reset when there is no object in the path of the raycast. This works in our case since in the scene there is only 1 gameObject that is Destroyable.
	=> When the sphere is hit, call the removeLife() function that is exposed from the SphereController.cs and check if the lives count is <= 0; after which we destroy the object. If not, we ignore it.
    * Only SphereController.cs must be the one keeping track of the lives remaining.
	=> SphereController.cs has one private variable called ‘lives’ which keeps track of the lives that the sphere has.




Credits for Shiba prefab: "Shiba" (https://skfb.ly/6WxVW) by zixisun02 is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
