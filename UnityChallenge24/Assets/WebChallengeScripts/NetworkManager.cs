using System.Net.Sockets;
using System.Collections;
using System.Text;
using System;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    //Data to be sent to the server via UDP
    [System.Serializable]
    private class UDP_Data
    {
        public float posX;
        public float posY;
        public float posZ;
        public float eulerX;
        public float eulerY;
        public float eulerZ;
        public float velX;
        public float velY;
        public float velZ;
        public float velAbs;
        public float time;

        public UDP_Data(float posX, float posY, float posZ,
                        float eulerX, float eulerY, float eulerZ,
                        float velX, float velY, float velZ, float velAbs, float time)
        {
            // <==== LEVEL 1: COMPLETE CONSTRUCTOR
        }
    }

    // RELEVANT UNITY OBJECTS
    public GameObject targetObject; // Object to track
    public Camera targetCamera; // Camera for image capture
    
    // SERVER PARAMETERS
    public string serverIP = "127.0.0.1"; // Server IP
    public int positionPort = 9090; // Port for positional data
    public int ImagePort = 9091; // <==== Hint for LEVEL 3
    public float positionPeriod = 0.5f; // seconds between sending position data to server

    private UdpClient positionClient; // <==== USE TO COMMUNICATE WITH SERVER

    void Start()
    {
        positionClient = new UdpClient();
        StartCoroutine(SendPositionCoroutine());
    }

    void OnApplicationQuit()
    {
        positionClient.Close(); //ALL UDP Connections should close here
    }

    private IEnumerator SendPositionCoroutine()
    {
        while (true)
        {
            if (targetObject != null)
            {
                SendPosition();
            }
            yield return new WaitForSeconds(positionPeriod);
        }
    }


    void SendPosition()
    {
        //<==== LEVEL 1: COMPLETE ...
    }


#region HELPER_FUNCTIONS

    void GetObjectPosition(GameObject obj, ref float x, ref float y, ref float z)
    {
        Vector3 position = obj.transform.position;
        x = position.x;
        y = position.y;
        z = position.z;
    }

    void GetObjectVelocity(GameObject obj, ref float velX, ref float velY, ref float velZ, ref float velAbs)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 velocity = rb.velocity;
            velX = velocity.x;
            velY = velocity.y;
            velZ = velocity.z;
            velAbs = velocity.magnitude;
        }
    }

    void GetObjectRotation(GameObject obj, ref float eulerX, ref float eulerY, ref float eulerZ)
    {
        Vector3 eulerAngles = obj.transform.eulerAngles;
        eulerX = eulerAngles.x;
        eulerY = (eulerAngles.y - 90) % 360;
        eulerZ = eulerAngles.z;
    }

    
    byte[] GetImageBytes(int resolutionWidth = 640, int resolutionHeight = 480, int jpegQuality = 50)
    {
        // Create a RenderTexture with a lower resolution
        RenderTexture renderTexture = new RenderTexture(resolutionWidth, resolutionHeight, 24);
        targetCamera.targetTexture = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    
        // Capture the image from the camera
        targetCamera.Render();
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
    
        // Encode the texture to a lower-quality JPEG
        byte[] imageBytes = texture.EncodeToJPG(jpegQuality);
    
        // Cleanup
        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
    
        return imageBytes;
    }

    #endregion
}
