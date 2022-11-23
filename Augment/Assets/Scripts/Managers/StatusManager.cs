using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public List<ChessPiece> [] bins = new List<ChessPiece> [6]; //each index corresponds to a statusCondition
    //0 stunned
    //1 trapped
    //2 ghost
    //3 constructing
    //4 beingConstructed
    //5 vanished

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bins.Length; i++)
        {
            List<ChessPiece> l = new List<ChessPiece>();
            bins[i] = l;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnUpdate()
    {
        for (int i = 0; i < bins.Length; i++)
        {
            for (int j = 0; j < bins[i].Count; j++)
            {
                ChessPiece cp = bins[i][j];
                cp.statusTimer--;
                if(cp.statusTimer <= 0)
                {
                    cp.statusTimer = 0;
                    cp.status = 0;
                    ReverseStatus(cp, i);
                    bins[i].Remove(cp);
                }
            }
            /*foreach (ChessPiece cp in bins[i])
            {
                cp.statusTimer--;
                if(cp.statusTimer <= 0)
                {
                    cp.statusTimer = 0;
                    cp.status = 0;
                    ReverseStatus(cp, i);
                    bins[i].Remove(cp);
                }
            }*/
        }
    }

    public void ApplyStatus(ChessPiece target, int status)
    {
        bins[status].Add(target);
        switch(status)
        {
            case 0:
                target.canMove = false;
            break;
            case 1:
                
            break;
            case 2:

            break;
            case 3:
                target.canMove = false;
            break;
            case 4:
                target.canMove = false;
            break;
            case 5:

            break;
        }
    }

    public void ReverseStatus(ChessPiece target, int status)
    {
        switch(status)
        {
            case 0:
                target.canMove = true;
            break;
            case 1:
            
            break;
            case 2:

            break;
            case 3:
                target.canMove = true;
            break;
            case 4:
                target.canMove = true;
            break;
            case 5:

            break;
        }
    }

    
}
