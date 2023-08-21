using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiva : Augmentor
{
    private bool augmentChanged = false;
    private Player piecePlayer;
    // Start is called before the first frame update
    void Start()
    {
        piecePlayer = GameManager.Instance.GetPlayer(this.gameObject.GetComponent<ChessPiece>().team);
    }

    
    public override void UseAugment()
    {
        //make it so that when you eat a piece, you get its augmentor component
        //copy the Augmentor component attached to the gameobject of the chess piece that you eat
        //then change this piece's augmentor to the new one
        

        /*
        if(!augmentChanged)
        {

            /*
            augmentChanged = true;
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
            Component aug = piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].gameObject.GetComponent<Augmentor>();
            this.augmentor = aug.augmentor;
            ChessPiece cp = this.gameObject.GetComponent<ChessPiece>();
            if(this.augmentor != null)
            {
                cp.pieceAugmentor = addedAug;
                if (this.gameObject.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
                {
                    s.color = cp.pieceAugmentor.augmentor.backgroundColor;
                }
            }
            else
            {
                if (this.gameObject.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
                {
                    s.color = new Vector4(1f, 1f, 1f, 0f); //no outline
                }
            }
            
            
        }*/
    }
}
