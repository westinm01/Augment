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
        
        server.Init(8008);
        client.Init("127.0.0.1", 8008);
    }
    public void OnOnlineConnectButton()
    {
        Debug.Log("Client init");
        
        client.Init(addressInput.text, 8008);
    }
    public void OnLocalGameButton()
    {
        server.Init(8007);
        client.Init("127.0.0.1", 8008);
    }
    public void OnHostBackButton()
    {
        server.Shutdown();
        client.Shutdown();
    }

}
