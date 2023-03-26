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
        Instantiate(fireBlock, new Vector3(0f,0f,0f), Quaternion.identity);
        FireBlock fire = fireBlock.GetComponent<FireBlock>();
        fire.coord = cp.coord;
        fire.team = cp.team;
        fire.turnCount = turnCount;
    }
}
