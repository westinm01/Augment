using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Jiva : Augmentor
{
    private bool augmentChanged = false;
    private Player piecePlayer;
    private GameObject managers;
    

    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[7];
        base.Awake();
    }
    void Start()
    {
        
        //augmentor = GameManager.Instance.GetHoldingManager().augmentorObjects[7];
        //UpdateInformation();
        //ChessPiece cp = this.gameObject.GetComponent<ChessPiece>();
    }

    
    public override void UseAugment()
    {
        piecePlayer = GameManager.Instance.GetPlayer(this.gameObject.GetComponent<ChessPiece>().team);
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
            TriggerManager tm = managers.GetComponent<TriggerManager>();
            tm.AddToBin(cp, newAugmentor.augmentor.triggerVal);
            //change outline color to augmentor's color
            if (this.gameObject.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
            {
                s.color = cp.pieceAugmentor.augmentor.backgroundColor;
            }
            Destroy(this);

        }
    }
}
