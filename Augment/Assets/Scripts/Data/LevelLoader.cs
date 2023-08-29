using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public int levelsCompleted;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("levelsCompleted", 5);
        levelsCompleted = PlayerPrefs.GetInt("levelsCompleted", 0);
        for(int i = 0; i <= levelsCompleted; i++)
        {
            GameObject level = transform.GetChild(0).GetChild(0).GetChild(i).gameObject; //get level
            Button levelButton = level.GetComponent<Button>(); // get level's button
            levelButton.interactable = true; // enable it
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
