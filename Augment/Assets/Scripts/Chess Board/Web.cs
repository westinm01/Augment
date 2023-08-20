using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : BoardObject
{
    StatusManager sm;
    GameObject managers;
    BoardManager bm;

    private void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        sm = managers.GetComponent<StatusManager>();
        bm = managers.GetComponent<BoardManager>();
    }

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = managers.GetComponent<HoldingManager>().augmentorEffectObjects[3].GetComponent<SpriteRenderer>().sprite;
    }
    
    void Update()
    {
        CheckForPiece();//this works, but doesn't handle for when the piece moves through the web
    }

    public void CheckForPiece()
    {
        if (sm.boardObjects.Contains(this))
        {
            ChessPiece cp = bm.GetChessPiece(coord.x, coord.y);
            if (cp != null)
            {
                if (cp.team != team)
                {
                    cp.status = 2;
                    cp.statusTimer = 6;
                    sm.ApplyStatus(cp, 0);
                    sm.boardObjects.Remove(this);
                    Destroy(gameObject);
                }
            }
        }
    }
}
