using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DukeNote : MonoBehaviour
{
    public float degreeOfRotation = 20f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += new Vector3(0.25f, 0.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //pinpong lerp between -10 and 10 degrees for rotation
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.PingPong(Time.time * degreeOfRotation, degreeOfRotation) - degreeOfRotation/2f);
    }
}
