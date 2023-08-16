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
    public List<BoardObject> boardObjects = new List<BoardObject>();
    public List<GameObject> otherObjects = new List<GameObject>();
    [SerializeField]
    private BoardManager cb;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bins.Length; i++)
        {
            List<ChessPiece> l = new List<ChessPiece>();
            bins[i] = l;
        }
        cb = FindObjectOfType<BoardManager>();//can assign in editor pretty easily
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
        }
        for(int i = 0; i < boardObjects.Count; i++)
        {
            boardObjects[i].turnCount--;
            if(boardObjects[i].turnCount == 0)
            {
                cb.RemovePiece(boardObjects[i].coord.x, boardObjects[i].coord.y);
                Destroy(boardObjects[i].gameObject);
                
            }
        }
        for(int i = 0; i < otherObjects.Count; i++)
        {
            otherObjects[i].GetComponent<TurnCount>().turnCount--;
            if(otherObjects[i].GetComponent<TurnCount>().turnCount == 0)
            {
                GameObject o = otherObjects[i];
                otherObjects.RemoveAt(i);
                Destroy(o);
                i--;
                
            }
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
                target.canMove = false;
            break;
            case 2:
                target.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, .6f);
            break;
            case 3:
                target.canMove = false;
            break;
            case 4:
                target.canMove = false;
            break;
            case 5:
                target.isVisible = false;
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
                target.canMove = true;
            break;
            case 2:
                //maybe make it fade first?
                Destroy(target.gameObject);
            break;
            case 3:
                target.canMove = true;
            break;
            case 4:
                target.canMove = true;
            break;
            case 5:
                target.isVisible = true;
            break;
        }
    }

    
}
