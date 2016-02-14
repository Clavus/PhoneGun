using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.Events;

public class TouchDeviceSocketListener : MonoBehaviour
{
    public string myIp = "192.168.0.104";
    public string defaultConnectIp = "192.168.0.100"; // Old Shield tablet
    //public string connectIp = "192.168.0.105"; // Nexus 7 tablet
    //public string connectIp = "192.168.0.100"; // Samsung phone
    //public string connectIp = "25.106.24.161";

    public Transform weapon;

    public UnityEvent fireAction;
    public UnityEvent reloadAction;
    
    private const string fireKeyword = "[FIRE]";
    private const string reloadKeyword = "[RELOAD]";
    private const string orientationKeyword = "[ORIENTATION]";
    private const string ipKeyword = "[IP] ";
    private const int listenPort = 29001;
    private const int sendPort = 29002;
    private Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private IPEndPoint sendIpEndPoint;
    private IPEndPoint listenIpEndPoint;
    private UdpClient listener;
    private Thread listenThread;

    private List<string> received = new List<string>();

    // Use this for initialization
    void Start ()
    {
        SetupUDPConnection();
    }
	
	// Update is called once per frame
	void Update () {

	    /*if (Input.GetKeyDown(KeyCode.Y))
	    {
	        //SendTCPData("Test data!");
            SendUDPData("Test data!");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SendUDPData(ipKeyword + myIp);
        }*/

        ProcessUDP();
	}

    public void SetupUDPConnection()
    {
        if (listenThread != null && listenThread.IsAlive)
            return;

        try
        {
            listener = new UdpClient(listenPort);
            listenIpEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            listenThread = new Thread(ReceiveUDP);
            listenThread.Start();
            Debug.Log("Set up UDP!");
        }
        catch (SocketException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void ConnectTo(string connectIp)
    {
        sendIpEndPoint = new IPEndPoint(IPAddress.Parse(connectIp), sendPort);
        udpSocket.Connect(sendIpEndPoint);
        SendUDPData(ipKeyword + myIp); // send our IP to the listening device, prompting it to start sending stuff our way

        Debug.Log("Send UDP connect message to " + connectIp + "!");
    }
    
    void OnApplicationQuit()
    {
        if (listenThread != null)
            listenThread.Abort();

        if (listener != null)
            listener.Close();

        udpSocket.Close();
    }

    private void SendUDPData(string str)
    {
        SendUDPData(Encoding.ASCII.GetBytes(str));
    }

    private void SendUDPData(byte[] data)
    {
        try
        {
            udpSocket.SendTo(data, sendIpEndPoint);
        }
        catch (SocketException e)
        {
            Debug.Log("Socket exception: " + e.Message);
        }

        Debug.Log("Sending " + data.Length + " bytes!" + Environment.NewLine);
    }
    
    private void ReceiveUDP()
    {
        bool done = false;
        listenIpEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        string received_data;
        byte[] receive_byte_array;
        try
        {
            while (!done)
            {
                // this is the line of code that receives the broadcase message.
                // It calls the receive function from the object listener (class UdpClient)
                // It passes to listener the end point groupEP.
                // It puts the data from the broadcast message into the byte array
                // named received_byte_array.
                // I don't know why this uses the class UdpClient and IPEndPoint like this.
                // Contrast this with the talker code. It does not pass by reference.
                // Note that this is a synchronous or blocking call.
                receive_byte_array = listener.Receive(ref listenIpEndPoint);
                received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                lock (received)
                {
                    received.Add(received_data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void ProcessUDP()
    {
        while (received.Count > 0)
        {
            string message;
            lock (received)
            {
                message = received[0];
                received.RemoveAt(0);
            }

            //Debug.Log("UDP message: " + message);

            if (message.Contains(fireKeyword))
            {
                //string task = message.Substring(message.IndexOf(stateKeyword) + stateKeyword.Length);
                fireAction.Invoke();
            }
            else if (message.Contains(reloadKeyword))
            {
                reloadAction.Invoke();
            }
            else if (message.Contains(orientationKeyword))
            {
                Debug.Log(message.Substring(message.IndexOf(orientationKeyword) + orientationKeyword.Length));
                Vector3 euler =
                    ParseVector3(
                        message.Substring(message.IndexOf(orientationKeyword) + orientationKeyword.Length).Trim());
                weapon.rotation =  Quaternion.Euler(-euler.x, -euler.y, euler.z);
            }
        }
    }

    private Vector3 ParseVector3(string rString)
    {
        string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
       
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        float z = float.Parse(temp[2]);
        Vector3 rValue = new Vector3(x, y, z);
        return rValue;
    }

}
