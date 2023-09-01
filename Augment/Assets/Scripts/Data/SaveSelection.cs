using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSelection : MonoBehaviour
{
    public int[] player1Augmentors = new int[6];
    public int[] player2Augmentors = new int[6];
    public bool player2IsHuman = true;//by default, player 2 is human
    public bool player1Ready = false;
    public bool player2Ready = false;
    public string[] AIAugmentors = new string[6];

    public GameObject panelAssign;

    private static SaveSelection _instance;

    public static SaveSelection Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    public void StartButton()
    {
        CaptureSelection();
        ConfirmSelection();
        if(player2IsHuman)
        {
            //reset the scene, but remember player1Augmentors
            //DontDestroyOnLoad(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
        }
        else
        {
            CaptureSelection();
            ConfirmSelection(); 
        }
        if(player1Ready && player2Ready)
        {
            DontDestroyOnLoad(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game Test");
        }
        
        
    }

    public void CaptureSelection()
    {
        panelAssign = GameObject.FindGameObjectsWithTag("PannelAssign")[0];
        if(!player1Ready)
        {
            for(int i = 0; i < 6; i++)
            {
                //GameObject g1 = panelAssign.transform.GetChild(0).gameObject;
                //GameObject g2 = g1.transform.GetChild(i).gameObject;
                //GameObject g3 = g2.transform.GetChild(0).gameObject;
                //GameObject g4 = g3.transform.GetChild(1).gameObject;
                //string augName = g4.GetComponent<TextMeshProUGUI>().text;
                string augName = panelAssign.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                player1Augmentors[i] = Translate(augName);
            }
            
            //populate player1Augmentors
        }
        else if (player2IsHuman)
        {
            //populate player2Augmentors
            for(int i = 0; i < 6; i++)
            {
                string augName = panelAssign.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                player2Augmentors[i] = Translate(augName);
            }
        }
        else
        {
            for(int i = 0; i < 6; i++)
            {
                player2Augmentors[i] = Translate(AIAugmentors[i]);
            }
            //populate player2Augmentors
        }
    }


    public void ConfirmSelection()
    {
        if(!player1Ready)
        {
            PlayerPrefs.SetInt("Pawn1", player1Augmentors[0]);
            PlayerPrefs.SetInt("Knight1", player1Augmentors[1]);
            PlayerPrefs.SetInt("Bishop1", player1Augmentors[2]);
            PlayerPrefs.SetInt("Rook1", player1Augmentors[3]);
            PlayerPrefs.SetInt("Queen1", player1Augmentors[4]);
            PlayerPrefs.SetInt("King1", player1Augmentors[5]);
            player1Ready = true;
            
            Debug.Log("PLAYER 1 AUGMENTORS:");
            Debug.Log(PlayerPrefs.GetInt("Pawn1"));
            Debug.Log(PlayerPrefs.GetInt("Knight1"));
            Debug.Log(PlayerPrefs.GetInt("Bishop1"));
            Debug.Log(PlayerPrefs.GetInt("Rook1"));
            Debug.Log(PlayerPrefs.GetInt("Queen1"));
            Debug.Log(PlayerPrefs.GetInt("King1"));
            
        }
        else
        {
            PlayerPrefs.SetInt("Pawn2", player2Augmentors[0]);
            PlayerPrefs.SetInt("Knight2", player2Augmentors[1]);
            PlayerPrefs.SetInt("Bishop2", player2Augmentors[2]);
            PlayerPrefs.SetInt("Rook2", player2Augmentors[3]);
            PlayerPrefs.SetInt("Queen2", player2Augmentors[4]);
            PlayerPrefs.SetInt("King2", player2Augmentors[5]);
            player2Ready = true;
            
            Debug.Log("PLAYER 2 AUGMENTORS:");
            Debug.Log(PlayerPrefs.GetInt("Pawn2"));
            Debug.Log(PlayerPrefs.GetInt("Knight2"));
            Debug.Log(PlayerPrefs.GetInt("Bishop2"));
            Debug.Log(PlayerPrefs.GetInt("Rook2"));
            Debug.Log(PlayerPrefs.GetInt("Queen2"));
            Debug.Log(PlayerPrefs.GetInt("King2"));
            
        }
    }

    private int Translate(string augName)
    {
        switch(augName)
        {
            case "Alejandra":
                return 0;
                break;
            case "Asabi":
                return 1;
                break;
            case "Doc":
                return 2;
                break;
            case "Duke":
                return 3;
                break;
            case "Felipe":
                return 4;
                break;
            case "Gabe":
                return 5;
                break;
            case "Hiroshi":
                return 6;
                break;
            case "Jiva":
                return 7;
                break;
            case "Lady LeMure":
                return 8;
                break;
            case "Lin":
                return 10;
                break;
            case "Lucy":
                return 11;
                break;
            case "Main & Solveig":
                return 12;
                break;
            case "Otto":
                return 13;
                break;
            case "Sali":
                return 14;
                break;  
            case "Tahira":
                return 15;
                break;
            case "Wagner":
                return 16;
                break;
            case "Vincente":
                return 17;
                break;
            default:
                return 0;
                break;
        }
    }
    
}
