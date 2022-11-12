using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamesWon : MonoBehaviour
{
    public TextMeshProUGUI gamesWon;
    public int winCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gamesWon.text = winCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWinCount()
    {
        //need game implement
    }
}
