using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public float fadeMultiplier = 1f;
    public float totalTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(new Vector4(1f,1f,1f,0f), new Vector4(1f,1f,1f,1f), Mathf.PingPong(Time.time * fadeMultiplier, 1));
    }
}
