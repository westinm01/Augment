using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public List<ChessPiece> [] bins = new List<ChessPiece> [8]; //each index corresponds to a triggerVal. ie. All ChessPieces in index 0 have an Augmentor triggerval of 0.
    private StatusManager sm;
    // Start is called before the first frame update
    void Start()
    {
        
        //populate bins with all pieces
        GameObject field = GameObject.FindGameObjectsWithTag("Field")[0];
        for (int i = 0; i < 7; i++)
        {
            List<ChessPiece> l = new List<ChessPiece>();
            bins[i] = l;
        }
        for (int i = 1; i < field.transform.childCount; i++)
        {
            ChessPiece cp = field.transform.GetChild(i).gameObject.GetComponent<ChessPiece>();
            int index = 0; 
            if (cp.pieceAugmentor != null)
            {
                index = cp.pieceAugmentor.triggerVal;
                index = 3;
            }
            Debug.Log(index);
            bins[index].Add(cp);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckTrigger(int triggerVal)
    {
        Debug.Log("Triggered");
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor != null)
            {
                cp.pieceAugmentor.UseAugment();//perform augment    
            }
                   
        }
    }
    public void CheckTrigger(int triggerVal, bool team)
    {
        Debug.Log("Triggered");
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor != null)
            {
                if(cp.team == team)
                {
                    cp.pieceAugmentor.UseAugment();//perform augment   
                }
                 
            }
        }
    }
    public void CheckTrigger(int triggerVal, ChessPiece piece)
    {
        Debug.Log("Triggered");
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor != null)
            {
                if(cp == piece)
                {
                    cp.pieceAugmentor.UseAugment();//perform augment    
                }
                
            }
        }
    }
    
}
