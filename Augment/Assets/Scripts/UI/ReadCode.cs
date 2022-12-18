using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCode : MonoBehaviour

{
    private string code;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetUsername(string inputCode)
    {
        code = inputCode;
        Debug.Log("Input Code:" + code);
    }
}
