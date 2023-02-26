using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int coins;
    void Awake()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
        TMP_Text coin_text = transform.GetChild(0).GetComponent<TMP_Text>();
        coin_text.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
