using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyLeMure : Augmentor
{
    // // Start is called before the first frame update
    // void Start()
    // {
    //     name = "Lady LeMure";
    //     backgroundColor = Color.purple;
    //     augmentName = "From The Grave";
    //     augmentDesc = "Upon eating an enemy piece, that piece becomes an ally Ghost for 2 turns. A ghost retains the piece's movement ability, but it does not retain its augmentation. It shares a space with its "killer" unless you chose to move it in your next turn.";
    //     triggerVal = 3;
    // }
    public AugmentorObject ghostAugment;
    private GameObject managers;

        protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[8];
        base.Awake();

    }

    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

        Player currPlayer = GameManager.Instance.GetCurrentPlayer();
        ChessPiece lastEaten = currPlayer.GetLastPieceEaten();

        // Remove the old augment
        lastEaten.gameObject.SetActive(true);
        if (lastEaten.pieceAugmentor != null) {
            lastEaten.pieceAugmentor.canActivate = false;
            lastEaten.pieceAugmentor.enabled = false;
        }

        // Add new ghost augment to destroy after 2 turns
        Ghost ghost = lastEaten.gameObject.AddComponent<Ghost>();
        //ghost.augmentor = ghostAugment;
        lastEaten.pieceAugmentor = ghost;
        ghost.UpdateInformation();
        TriggerManager tm = GameManager.Instance.GetTriggerManager();
        tm.AddToBin(lastEaten, ghost.triggerVal);
        
        StatusManager sm = managers.GetComponent<StatusManager>();
        lastEaten.statusTimer = 6; //ghost is alive for 6 turns
        sm.ApplyStatus(lastEaten, 2); // apply ghost status
        lastEaten.SwitchTeams();
        //sometimes doesn't switch turns
    }
}
