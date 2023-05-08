using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public List<ChessPiece> [] bins = new List<ChessPiece> [9]; //each index corresponds to a triggerVal. ie. All ChessPieces in index 0 have an Augmentor triggerval of 0.
    private StatusManager sm;
    // Start is called before the first frame update
    void Start()
    {
        
        //populate bins with all pieces
        GameObject field = GameObject.FindGameObjectsWithTag("Field")[0];
        for (int i = 0; i < 8; i++)
        {
            List<ChessPiece> l = new List<ChessPiece>();
            bins[i] = l;
        }
        for (int i = 1; i < field.transform.childCount; i++)
        {
            ChessPiece cp = field.transform.GetChild(i).gameObject.GetComponent<ChessPiece>();
            int index = 7;
            //this area is causing major issues. Triggers dont work when expected. 
            if (cp && cp.pieceAugmentor != null)
            {
                Debug.Log("Actually added Augmentor: " + cp.pieceAugmentor.name);
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
                    cp.pieceAugmentor.UseAugment();//perform augment   
                }
                 
            }
        }
    }

    public void AddToBin(ChessPiece newPiece, int triggerVal)
    {
        if (bins[triggerVal] == null) {
            bins[triggerVal] = new List<ChessPiece>();
        }
        if (bins[triggerVal].Contains(newPiece))
        {
            return;
        }
        bins[triggerVal].Add(newPiece);
    }

    public void RemoveFromBin(ChessPiece piece, int triggerVal)
    {
        if (bins[triggerVal].Contains(piece))
        {
            bins[triggerVal].Remove(piece);
        }
        
    }
    
}
