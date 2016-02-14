using UnityEngine;
using System.Collections;

public class ConnectIPHUD : SingletonComponent<ConnectIPHUD>
{

    public int offsetX;
    public int offsetY;
    public TouchDeviceSocketListener listener;

    private bool showGUI = true;
    private string connectIp = "";

    private float messageShowPeriod = 3;
    private float messageHideTime;
    private string message = "";
    private int inputFrameLag;

    private void Start()
    {
        if (instance != this)
            Destroy(gameObject);
        
        if (listener != null)
            connectIp = listener.defaultConnectIp;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
            showGUI = true;
    }

    private void OnGUI()
    {
        if (listener == null || !showGUI)
            return;

        int num = 10 + this.offsetX;
        int num2 = 40 + this.offsetY;

        GUI.Label(new Rect((float)num, (float)num2, 200f, 20f), "<< DEVICE CONNECTING >>");
        num2 += 24;
        GUI.Label(new Rect((float) num, (float) num2, 105f, 20f), "Listen IP");
        listener.myIp = GUI.TextField(new Rect((float) (num + 100), (float) num2, 95f, 20f), listener.myIp);
        num2 += 24;
        GUI.Label(new Rect((float) num, (float) num2, 105f, 20f), "Connect IP");
        connectIp = GUI.TextField(new Rect((float) (num + 100), (float) num2, 95f, 20f), connectIp);
        num2 += 24;
        if (GUI.Button(new Rect((float) num, (float) num2, 200f, 20f), "Establish connection..."))
        {
            listener.ConnectTo(connectIp);
        }
        if (Time.unscaledTime < messageHideTime)
        {
            num2 += 24;
            GUI.Label(new Rect((float)num, (float)num2, 200f, 20f), "<< " + message + " >>");
        }
        num2 = Screen.height - 24;
        if (GUI.Button(new Rect((float) num, (float) num2, 200f, 20f), "Close GUI"))
        {
            showGUI = false;
            //vrui.LockMouse(true);
        }
    }

    public void TriggerMessage(string msg)
    {
        messageHideTime = Time.unscaledTime + messageShowPeriod;
        message = msg;
    }

    public void SetInputFrameLag(int i)
    {
        inputFrameLag = i;
    }
}
