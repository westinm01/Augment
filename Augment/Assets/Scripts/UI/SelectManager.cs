using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public int currPiece; //0-Pawn 1-Knight 2-Bishop 3-Rook 4-Queen 5-King
    private float timePassed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddAugmentor(GameObject augmentor)
    {
        GameObject parent = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(currPiece).GetChild(0).gameObject;
        GameObject imageObject = parent.transform.GetChild(0).GetChild(0).gameObject;
        GameObject textObject = parent.transform.GetChild(1).gameObject;
        textObject.GetComponent<TextMeshProUGUI>().text = augmentor.transform.parent.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
        imageObject.GetComponent<Image>().sprite = augmentor.transform.GetChild(0).GetComponent<Image>().sprite;
        parent.transform.GetChild(0).gameObject.GetComponent<Image>().color = augmentor.GetComponent<Image>().color;
    }

    public void SetCurrPiece(int pieceNum)
    {
        currPiece = pieceNum;
    }
    
    
}
