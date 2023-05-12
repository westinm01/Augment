using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiroshi : Augmentor
{
    // // Start is called before the first frame update
    // void Start()
    // {
    //     hasPrompt = true;
    //     name = "Hiroshi";
    //     backgroundColor = Color.grey;
    //     augmentName = "Vanish";
    //     augmentDesc = "This piece can vanish from the board for 3 turns. After, it eats any piece on its space, but if an ally is on its space, this piece is discarded.";
    //     triggerVal = 2;
    // }

    private bool isInvis;
    private int turnsLeft;

    protected override void Start() {
        base.Start();
        isInvis = false;
        turnsLeft = 0;   
        canActivate = true;
    }

    public void Wait() {
        if (turnsLeft == 0) {
            Debug.Log("Finished waiting");
            UseAugment();
        }
        else {
            turnsLeft--;
        }
    }

    public override void UseAugment()
    {
        if (!isInvis) {
            Debug.Log("Activating Hiroshi");
            isInvis = true;
            turnsLeft = 6;  // 3 player turns invisible
            canActivate = false;

            GameManager.Instance.GetEventsManager().OnTurnEnd += Wait;
            augmentPiece.BanishPiece();
        }
        else {
            augmentPiece.UnbanishPiece();
            GameManager.Instance.GetEventsManager().OnTurnEnd -= Wait;
            isInvis = false;
            canActivate = true;
        }
    }
}
