using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField]
    private GameObject augmentPanel;
    [SerializeField]
    private GameObject useAugmentButton;


    [Header("AugmentorInfo")]
    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    private TextMeshProUGUI augmentName;
    [SerializeField]
    private TextMeshProUGUI augmentDescription;
    [SerializeField]
    private SpriteRenderer characterPortrait;
    [SerializeField]
    private SpriteRenderer background;
    

    private ChessPiece currSelected;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    public void ActivateAugmentPrompt(ChessPiece piece) {
        // Deactivate first in case piece doesn't have augment
        DeactivateAugmentPrompt();

        if (piece.pieceAugmentor == null) {
            // piece has no attached augment or augment is passive
            // do nothing
            return;
        }

        characterName.text = piece.pieceAugmentor.characterName;
        augmentName.text = piece.pieceAugmentor.augmentName;
        augmentDescription.text = piece.pieceAugmentor.augmentDesc;
        characterPortrait.sprite = piece.pieceAugmentor.sprite;
        background.color = piece.pieceAugmentor.backgroundColor;

        if (piece.pieceAugmentor.hasPrompt) {
            useAugmentButton.SetActive(true);
            useAugmentButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use Augment";
        }

        augmentPanel.SetActive(true);
        currSelected = piece;
    }

    public void DeactivateAugmentPrompt() {
        currSelected = null;
        useAugmentButton.SetActive(false);
        augmentPanel.SetActive(false);
    }

    public void ClickUseAugment() {
        currSelected.pieceAugmentor.UseAugment();
        useAugmentButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";

        // Remove this later, call function in InputManager after getting the required input
        DeactivateAugmentPrompt();
    }
}
