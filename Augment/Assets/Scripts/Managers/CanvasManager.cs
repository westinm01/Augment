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
    private GameObject checkmatePopupText;    
    [SerializeField]
    private GameObject checkmatePopupBackground;
    [SerializeField]
    private float checkPopupLingerTime;    // How long the popup slows for
    [SerializeField]
    private float fastPopupMoveSpeed;   // How fast the text/background moves to the middle/away
    [SerializeField]
    private float slowPopupMoveSpeed;   // How fast the text/background moves in the middle of the screen

    [Header("Augmentor Flash")]
    [SerializeField]
    private GameObject augmentorFlashSprite;
    [SerializeField]
    private float fastAugmentorFlashMoveSpeed;   // How fast the augmentor moves to the middle/away
    [SerializeField]
    private float slowAugmentorFlashMoveSpeed;   // How fast the augmentor moves in the middle of the screen

    [Header("Timers")]
    [SerializeField]
    private TextMeshProUGUI whiteTotalTimer;
    [SerializeField]
    private TextMeshProUGUI blackTotalTimer;
    [SerializeField]
    private TextMeshProUGUI currTimer;


    private ChessPiece currSelected;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Update()
    {
        UpdateTimers();
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
        bool currPlayer = GameManager.Instance.GetCurrentPlayer().playerTeam;
        if (piece.pieceAugmentor.hasPrompt && piece.team == currPlayer && piece.pieceAugmentor.canActivate) {
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
        checkPopupText.SetActive(true);
        checkPopupBackground.SetActive(true);
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

    public IEnumerator CheckmateCoroutine() {
        checkPopup.SetActive(true);
        checkmatePopupText.SetActive(true);
        checkmatePopupBackground.SetActive(true);
        Vector3 textOrigin = checkmatePopupText.transform.position;
        Vector3 backgroundOrigin = checkmatePopupBackground.transform.position;

        // Text flies in from the right, background flies in from the left
        // Text moving towards middle fast
        // Add a slight buffer to the mid point to prevent sliding too far
        while (checkmatePopupText.transform.localPosition.x > 100 && checkmatePopupBackground.transform.localPosition.x < -100) {
            checkmatePopupText.transform.position -= new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkmatePopupBackground.transform.position += new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        // Text moving in middle slowly
        float slowTimer = 0;
        while (slowTimer < checkPopupLingerTime) {
            checkmatePopupText.transform.position -= new Vector3(slowPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkmatePopupBackground.transform.position += new Vector3(slowPopupMoveSpeed * Time.deltaTime, 0, 0);
            slowTimer += Time.deltaTime;
            yield return null;
        }

        // Text moving away from middle fast
        while (checkmatePopupText.transform.position.x > backgroundOrigin.x && checkmatePopupBackground.transform.position.x < textOrigin.x) {
            checkmatePopupText.transform.position -= new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            checkmatePopupBackground.transform.position += new Vector3(fastPopupMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }


        checkmatePopupText.transform.position = textOrigin;
        checkmatePopupBackground.transform.position = backgroundOrigin;
        checkPopup.SetActive(false);
    }

    public IEnumerator AugmentorFlash(Augmentor augmentor)
    {
        augmentorFlashSprite.SetActive(true);
        augmentorFlashSprite.GetComponent<SpriteRenderer>().sprite = augmentor.sprite;

        //Vector3 augmentorOrigin = augmentorFlashSprite.transform.position;
        Vector3 augmentorOrigin = new Vector3(1000, 0, 0);
        Vector3 destination = augmentorOrigin;
        destination.x = -destination.x;

        // Augmentor flies in from the right
        // Augmentor moving towards middle fast
        // Add a slight buffer to the mid point to prevent sliding too far
        while (augmentorFlashSprite.transform.localPosition.x > 100) {
            augmentorFlashSprite.transform.position -= new Vector3(fastAugmentorFlashMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        // Augmentor moving in middle slowly
        augmentor.PlayRandomBark();
        AudioManager am = GameManager.Instance.GetAudioManager();
        while (am.IsPlayingFX()) {
            augmentorFlashSprite.transform.position -= new Vector3(slowAugmentorFlashMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        // Augmentor moving away from middle fast
        while (augmentorFlashSprite.transform.localPosition.x > -1000) {
            augmentorFlashSprite.transform.position -= new Vector3(fastAugmentorFlashMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        augmentorFlashSprite.transform.localPosition = augmentorOrigin;
        augmentorFlashSprite.SetActive(false);
    }

    private void UpdateTimers() {
        float whiteTimer = GameManager.Instance.GetPlayer(true).totalTimer;
        float blackTimer = GameManager.Instance.GetPlayer(false).totalTimer;

        whiteTotalTimer.text = ConvertTimerToString(whiteTimer);
        blackTotalTimer.text = ConvertTimerToString(blackTimer);
        currTimer.text = ConvertTimerToString(GameManager.Instance.turnTimer);
    }

    private string ConvertTimerToString(float time)
    {
        int minutes = (int) time / 60;
        float seconds = time - (minutes * 60f);
        seconds = (int)(seconds * 100) / 100f;  // Truncate to 2 decimal points
        return minutes + ":" + seconds;
    }
}
