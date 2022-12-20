using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using System;
using Unity.Collections;

public class Client : MonoBehaviour
{
    #region Singleton Implementation
    public static Client instance {set; get;}

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public NetworkDriver driver;
    private NetworkConnection connection;

    private bool isActive = false;
 
    public Action connectionDropped;

    public void Init(string ip, ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(ip, port);
        
        connection = driver.Connect(endpoint);
        Debug.Log("Attempting to connect to Server on " + endpoint.Address);
        isActive = true;

        RegisterToEvent();
    }

    public void Shutdown()
    {
        if (isActive)
        {
            UnregisterToEvent();
            driver.Dispose();
            isActive = false;
            connection = default(NetworkConnection);
        }
    }

    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }
 
        driver.ScheduleUpdate().Complete();
        CheckAlive();

        UpdateMessagePump();

    }
    private void CheckAlive()
    {
        if (!connection.IsCreated && isActive)
        {
            Debug.Log("Something went wrong, lost connection to server");
            connectionDropped?.Invoke();
            Shutdown();
        }
    }

    private void UpdateMessagePump()
    {
        
        DataStreamReader stream;
        NetworkEvent.Type cmd = connection.PopEvent(driver, out stream);
        //Debug.Log(cmd);
        while(cmd != NetworkEvent.Type.Empty) 
        {
            
           if (cmd == NetworkEvent.Type.Connect)
           {
            //SendToServer(new NetWelcome());
            Debug.Log("We're connected!");
           }
           else if (cmd == NetworkEvent.Type.Data)
           {
                NetUtility.OnData(stream, default(NetworkConnection));
           }
           else if (cmd == NetworkEvent.Type.Disconnect)
           {
                Debug.Log("Client got disconnected from server");
                connection = default(NetworkConnection);
                connectionDropped?.Invoke();
                Shutdown();
           }
        }
        
    }

    private void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    //event parsing
    private void RegisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE += OnKeepAlive;
    }
    private void UnregisterToEvent()
    {
       NetUtility.C_KEEP_ALIVE -= OnKeepAlive;
    }
    private void OnKeepAlive(NetMessage nm)
    {
        SendToServer(nm);
    }

}
