using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabeElectricity : MonoBehaviour
{
    private GameManager managers;
    public float timer = 1f;
    private float timePassed = 0f;
    bool small = false;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= timer)
        {
            if(!small)
            {
                small = true;
                
                gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            }
            else
            {
                small = false;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            timePassed = 0;
        }
    }
}
