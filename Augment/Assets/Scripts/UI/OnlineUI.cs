using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OnlineUI : MonoBehaviour
{
    public Server server;
    public Client client;
    public TMP_InputField addressInput;

    public void OnOnlineHostButton()
    {
        
        server.Init(80);
        client.Init("127.0.0.1", 80);
    }
    public void OnOnlineConnectButton()
    {
        Debug.Log("Client init");
        
        client.Init(addressInput.text, 80);
    }
    public void OnLocalGameButton()
    {
        server.Init(80);
        client.Init("127.0.0.1", 80);
    }
    public void OnHostBackButton()
    {
        server.Shutdown();
        client.Shutdown();
    }

}
