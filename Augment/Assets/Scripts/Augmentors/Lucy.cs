using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : Augmentor
{
    public ChessPiece fireBlock;
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        Instantiate(fireBlock, new Vector3(0f,0f,0f), Quaternion.identity);
        //fireBlock.GetComponent<Fire>().isTimerOn = true;
        fireBlock.coord = cp.coord;
        fireBlock.team = cp.team;
    }
}
