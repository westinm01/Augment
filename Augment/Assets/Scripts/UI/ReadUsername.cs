using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadUsername : MonoBehaviour
    
{   
    private string input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUsername(string Username)
    {
        input = Username;
        Debug.Log("Input Username:" + input);
    }
}
