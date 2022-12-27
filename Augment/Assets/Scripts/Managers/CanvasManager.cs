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
    
    [Header("Check Popup")]
    [SerializeField]
    private GameObject checkPopup;
    [SerializeField]
    private GameObject checkPopupText;    
    [SerializeField]
    private GameObject checkPopupBackground;
    [SerializeField]
    private float checkPopupLingerTime;    // How long the popup slows for
    [SerializeField]
    private float fastPopupMoveSpeed;   // How fast the text/background moves to the middle/away
    [SerializeField]
    private float slowPopupMoveSpeed;   // How fast the text/background moves in the middle of the screen
    


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

    public IEnumerator CheckCoroutine() {
        checkPopup.SetActive(true);
        Vector3 textOrigin = checkPopupText.transform.position;
        Vector3 backgroundOrigin = checkPopupBackground.transform.position;

        // Text flies in from the right, background flies in from the left
        // Text moving towards middle fast
        // Add a slight buffer to the mid point to prevent sliding too far
        while (checkPopupText.transform.localPosition.x > 100 && checkPopupBackground.transform.localPosition.x < -100) {
            checkPopupText.transform.position -= new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkPopupBackground.transform.position += new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        // Text moving in middle slowly
        float slowTimer = 0;
        while (slowTimer < checkPopupLingerTime) {
            checkPopupText.transform.position -= new Vector3(slowPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkPopupBackground.transform.position += new Vector3(slowPopupMoveSpeed * Time.deltaTime, 0, 0);
            slowTimer += Time.deltaTime;
            yield return null;
        }

        // Text moving away from middle fast
        while (checkPopupText.transform.position.x > backgroundOrigin.x && checkPopupBackground.transform.position.x < textOrigin.x) {
            checkPopupText.transform.position -= new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkPopupBackground.transform.position += new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }


        checkPopupText.transform.position = textOrigin;
        checkPopupBackground.transform.position = backgroundOrigin;
        checkPopup.SetActive(false);
    }
}
