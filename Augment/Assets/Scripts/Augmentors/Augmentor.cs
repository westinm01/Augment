using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augmentor : MonoBehaviour
{
    [Header("AugmentorObject")] // Filled out automatically by ScriptableObject
    [HideInInspector]
    public string characterName;
    [HideInInspector]
    public string augmentName;
    [HideInInspector]
    public string augmentDesc;
    [HideInInspector]
    public Sprite sprite;
    [HideInInspector]
    public Color backgroundColor;
    [HideInInspector]
    public int triggerVal;
    [HideInInspector]
    public bool hasPrompt = false;
    [HideInInspector]

    [Header("Class variables")]
    public ChessPiece augmentPiece;
    public ChessPiece targetPiece;
    public AugmentorObject augmentor;
    public bool canActivate = true;
    [HideInInspector]
    public List<AudioClip> barks;

    protected virtual void Awake()
    {
        UpdateInformation();
    }

    protected virtual void Start(){
        UpdateInformation();
    }

    public void UpdateInformation() {
        this.characterName = augmentor.characterName;
        this.augmentName = augmentor.augmentName;
        this.augmentDesc = augmentor.description;
        this.sprite = augmentor.sprite;
        this.backgroundColor = augmentor.backgroundColor;
        this.backgroundColor.a = 1;
        this.triggerVal = augmentor.triggerVal;
        this.hasPrompt = augmentor.hasPrompt;
        this.barks = augmentor.barks;

        this.augmentPiece = GetComponent<ChessPiece>();
    }

    public virtual void UseAugment()
    {
        if(canActivate)
        {
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
    }

    public virtual void UseAugment(Vector2Int space)
    {
        if(canActivate)
        {
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
    }

    public virtual void UseAugment(ChessPiece piece)
    {
        if(canActivate)
        {
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
    } 

    public virtual void PlayVFX()
    {
        //play animation or particle effects.
    }
    public virtual void PlayRandomBark()
    {
        //play random audio clip from barks.
        if (barks.Count > 0)
        {
            int randIndex = Random.Range(0, barks.Count);
            GameManager.Instance.GetAudioManager().PlaySound(barks[randIndex]);
        }
    }
    public virtual void PlaySFX()
    {
        
    }
}
