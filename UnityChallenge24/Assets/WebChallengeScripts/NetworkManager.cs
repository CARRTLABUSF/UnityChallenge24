using System.Net.Sockets;
using System.Collections;
using System.Text;
using System;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
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
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.eulerX = eulerX;
            this.eulerY = eulerY;
            this.eulerZ = eulerZ;
            this.velX = velX;
            this.velY = velY;
            this.velZ = velZ;
            this.velAbs = velAbs;
            this.time = time;
        }
    }

    public GameObject targetObject;
    public Camera targetCamera;

    public string serverIP = "127.0.0.1";
    public int positionPort = 9090;
    public int imagePort = 9091;

    public float positionPeriod = 0.5f;
    private UdpClient positionClient;
    private UdpClient imageClient;

    void Start()
    {
        positionClient = new UdpClient();
        imageClient = new UdpClient();
        
        StartCoroutine(SendPositionCoroutine());
        StartCoroutine(SendImageCoroutine());
    }

    void OnApplicationQuit()
    {
        positionClient.Close();
        imageClient.Close();
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
        float posX = 0, posY = 0, posZ = 0;
        float eulerX = 0, eulerY = 0, eulerZ = 0;
        float velX = 0, velY = 0, velZ = 0, velAbs = 0;
        float time = Time.time;

        GetObjectPosition(targetObject, ref posX, ref posY, ref posZ);
        GetObjectRotation(targetObject, ref eulerX, ref eulerY, ref eulerZ);
        GetObjectVelocity(targetObject, ref velX, ref velY, ref velZ, ref velAbs);

        UDP_Data data = new UDP_Data(posX, posY, posZ, eulerX, eulerY, eulerZ, velX, velY, velZ, velAbs, time);
        string jsonData = JsonUtility.ToJson(data);

        byte[] sendBytes = Encoding.UTF8.GetBytes(jsonData);
        positionClient.Send(sendBytes, sendBytes.Length, serverIP, positionPort);
    }

    private IEnumerator SendImageCoroutine()
    {
        while (true)
        {
            byte[] imageBytes = GetImageBytes(320, 240, 30);
            SendImage(imageBytes);
            yield return new WaitForSeconds(1f);
        }
    }

    void SendImage(byte[] imageBytes)
    {
        int chunkSize = 1024;
        int totalChunks = Mathf.CeilToInt((float)imageBytes.Length / chunkSize);

        for (int i = 0; i < totalChunks; i++)
        {
            int offset = i * chunkSize;
            int length = Mathf.Min(chunkSize, imageBytes.Length - offset);

            byte[] chunk = new byte[length];
            Array.Copy(imageBytes, offset, chunk, 0, length);

            imageClient.Send(chunk, chunk.Length, serverIP, imagePort);
        }
    }

    byte[] GetImageBytes(int resolutionWidth = 320, int resolutionHeight = 240, int jpegQuality = 30)
    {
        RenderTexture renderTexture = new RenderTexture(resolutionWidth, resolutionHeight, 24);
        targetCamera.targetTexture = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        targetCamera.Render();
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        byte[] imageBytes = texture.EncodeToJPG(jpegQuality);

        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        return imageBytes;
    }

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
}
