using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private Transform targetObject; 
    private UdpClient udpClient;
    private string serverIP = "127.0.0.1"; 
    private int serverPort = 9090; 

    void Start()
    {
        udpClient = new UdpClient();
        Debug.Log($"UDP Client initialized. Sending data to {serverIP}:{serverPort}");
    }

    void Update()
    {
        SendPosition(); 
    }

    public void SendPosition()
    {
        if (targetObject == null) return;

        // Get data from the object
        Vector3 position = GetPosition();
        Vector3 velocity = GetVelocity();
        Quaternion rotation = GetRotation();

        UDP_Data data = new UDP_Data
        {
            position = position,
            velocity = velocity,
            rotation = rotation
        };

        // Convert to JSON
        string jsonData = JsonUtility.ToJson(data);

        byte[] sendBytes = Encoding.UTF8.GetBytes(jsonData);
        udpClient.Send(sendBytes, sendBytes.Length, serverIP, serverPort);

        Debug.Log($"Sent Data to {serverIP}:{serverPort} → {jsonData}");
    }

    Vector3 GetPosition()
    {
        return targetObject.position;
    }

    Vector3 GetVelocity()
    {
        Rigidbody rb = targetObject.GetComponent<Rigidbody>();
        return rb != null ? rb.velocity : Vector3.zero;
    }

    Quaternion GetRotation()
    {
        return targetObject.rotation;
    }
}

// Data Structure for UDP Transmission
[System.Serializable]
public class UDP_Data
{
    public Vector3 position;
    public Vector3 velocity;
    public Quaternion rotation;
}
