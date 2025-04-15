using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class NetworkClient : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    Thread receiveThread;
    
    public GameManager gameManager;
    public string serverIP = "127.0.0.1"; // Default to localhost
    public int serverPort = 8888;         // Default port

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            
            if (client.Connected)
            {
                Debug.Log("Connected to server at " + serverIP + ":" + serverPort);
                stream = client.GetStream();
                receiveThread = new Thread(receiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            else
            {
                Debug.LogError("Failed to connect to server");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    public void SendMessage(string msg)
    {
        if (client != null && client.Connected && stream != null)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("Error sending message: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Cannot send message: Not connected to server");
        }
    }

    public void receiveMessages()
    {
        byte[] data = new byte[1024];
        try
        {
            while (client != null && client.Connected)
            {
                int bytes = stream.Read(data, 0, data.Length);
                if (bytes > 0)
                {
                    string msg = Encoding.UTF8.GetString(data, 0, bytes);
                    Debug.Log("Server says: " + msg);
                    
                    // Use Unity's main thread to update UI
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        gameManager.ReceiveMessageServer(msg);
                    });
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving message: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
        receiveThread?.Abort();
    }
}
