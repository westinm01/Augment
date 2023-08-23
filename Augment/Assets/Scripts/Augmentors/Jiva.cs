using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Jiva : Augmentor
{
    private bool augmentChanged = false;
    private Player piecePlayer;

    void Start()
    {
        piecePlayer = GameManager.Instance.GetPlayer(this.gameObject.GetComponent<ChessPiece>().team);
    }

    
    public override void UseAugment()
    {
        augmentChanged = true;
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameObject source = piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].gameObject;
        if (source.TryGetComponent(out Augmentor sourceAugmentor))
        {
            Type sourceType = sourceAugmentor.GetType();
            Component replacedAugmentor = this.gameObject.AddComponent(sourceType);
            Augmentor newAugmentor = replacedAugmentor as Augmentor;
            newAugmentor.augmentor = sourceAugmentor.augmentor;
            ChessPiece cp = this.gameObject.GetComponent<ChessPiece>();
            cp.pieceAugmentor = newAugmentor;
            newAugmentor.UpdateInformation();
            //change outline color to augmentor's color
            if (this.gameObject.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
            {
                s.color = cp.pieceAugmentor.augmentor.backgroundColor;
            }
            Destroy(this);

        }
    }
}
