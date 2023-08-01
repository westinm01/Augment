using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : Augmentor
{
    public GameObject fireBlock;
    public int turnCount = 2;
    int lastCol = 0;
    int lastRow = 0;
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        GameObject g = Instantiate(fireBlock, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10), Quaternion.identity);
        //GameObject g = Instantiate(fireBlock, new Vector3(0,0,-1), Quaternion.identity);
        g.transform.parent = this.gameObject.transform.parent;
        FireBlock fire = g.GetComponent<FireBlock>();
        //fire.coord = new Vector2(GetComponent<ChessPiece>().coord.x, GetComponent<ChessPiece>().coord.y);
        StatusManager sm = FindObjectOfType<StatusManager>();
        sm.boardObjects.Add(fire);
        fire.coord = cp.coord;
        fire.team = !(cp.team);
        fire.turnCount = turnCount;
        BoardManager cb = FindObjectOfType<BoardManager>();
        Debug.Log("Fire coord: " + fire.coord.x + ", " + fire.coord.y);
        cb.AddPiece(fire, fire.coord.x, fire.coord.y);
    }
}
