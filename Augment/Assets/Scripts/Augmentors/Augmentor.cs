using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augmentor : MonoBehaviour
{
    [Header("AugmentorObject")]
    public string characterName;
    public string augmentName;
    public string augmentDesc;
    public Sprite sprite;
    public Color backgroundColor;
    public int triggerVal;
    public bool hasPrompt = false;

    [Header("Class variables")]
    public ChessPiece augmentPiece;
    public AugmentorObject augmentor;
    public bool canActivate = false;
    public List<AudioClip> barks;

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
