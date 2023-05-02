using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : Augmentor
{
    public GameObject fireBlock;
    public int turnCount = 2;
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        GameObject g = Instantiate(fireBlock, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -1), Quaternion.identity);
        
        g.transform.parent = this.gameObject.transform.parent;
        FireBlock fire = g.GetComponent<FireBlock>();

        StatusManager sm = FindObjectOfType<StatusManager>();
        sm.boardObjects.Add(fire);
        fire.coord = cp.coord;
        fire.team = cp.team;
        fire.turnCount = turnCount;
    }
}
