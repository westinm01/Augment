using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public List<List<ChessPiece>> bins = new List<List<ChessPiece>>(); //each index corresponds to a triggerVal. ie. All ChessPieces in index 0 have an Augmentor triggerval of 0.
    
    // Start is called before the first frame update
    void Start()
    {
        //need to populate bins.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckTrigger(int triggerVal)
    {
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor)
            {
                cp.pieceAugmentor.UseAugment();//perform augment    
            }
                   
        }
    }
    public void CheckTrigger(int triggerVal, bool team)
    {
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor)
            {
                cp.pieceAugmentor.UseAugment();//perform augment    
            }
        }
    }
    public void CheckTrigger(int triggerVal, ChessPiece piece)
    {
        foreach (ChessPiece cp in bins[triggerVal])
        {
            if(cp.pieceAugmentor)
            {
                cp.pieceAugmentor.UseAugment();//perform augment    
            }
        }
    }
    
}
