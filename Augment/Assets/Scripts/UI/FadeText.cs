using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeText : MonoBehaviour
{  
    public float stillTime = 2.0f;
    public float fadeTime = 1.0f;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //interpolate text color from alpha = 255 to 0
        //destroy object when alpha = 0
        if(stillTime > 0)
        {
            stillTime -= Time.deltaTime;
        }
        else
        {
            text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 0), fadeTime * Time.deltaTime);
        }
    }
}
