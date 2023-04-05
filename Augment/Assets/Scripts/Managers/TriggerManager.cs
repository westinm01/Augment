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
            int index = 3; 
            if (cp && cp.pieceAugmentor != null)
            {
                index = cp.pieceAugmentor.triggerVal;
                //index = 3;
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
        
        if(bins[triggerVal] == null)
        {
            return;
        }
         for(int i = 0; i < bins[triggerVal].Count; i++)
        {
            ChessPiece cp = bins[triggerVal][i];
            if(cp && cp.pieceAugmentor != null)
            {
                Debug.Log("Triggered: " + triggerVal);
                cp.pieceAugmentor.UseAugment();//perform augment   
            }
        }
    }
    public void CheckTrigger(int triggerVal, bool team)
    {
        
        if(bins[triggerVal] == null)
        {
            return;
        }
        for(int i = 0; i < bins[triggerVal].Count; i++)
        {
            ChessPiece cp = bins[triggerVal][i];
            if(cp && cp.pieceAugmentor != null)
            {
                if(cp.team == team)
                {
                    // Debug.Log("Triggered" + triggerVal);
                    // cp.pieceAugmentor.UseAugment();//perform augment   
                }
                 
            }
        }
    }
    public void CheckTrigger(int triggerVal, ChessPiece piece)
    {
        
        if(bins[triggerVal] == null)
        {
            return;
        }
        for(int i = 0; i < bins[triggerVal].Count; i++)
        {
            ChessPiece cp = bins[triggerVal][i];
            if(cp && cp.pieceAugmentor != null)
            {
                if(cp == piece)
                {
                    // Debug.Log("Triggered//" + triggerVal);
                    // cp.pieceAugmentor.UseAugment();//perform augment   
                }
                 
            }
        }
    }

    public void AddToBin(ChessPiece newPiece, int triggerVal)
    {
        if (bins[triggerVal] == null) {
            bins[triggerVal] = new List<ChessPiece>();
        }
        bins[triggerVal].Add(newPiece);
    }
    
}
