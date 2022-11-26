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
    public List<AudioClip> barks = new List<AudioClip>();

    protected void Start(){
        UpdateInformation();
    }

    public void UpdateInformation() {
        this.characterName = augmentor.characterName;
        this.augmentName = augmentor.augmentName;
        this.augmentDesc = augmentor.description;
        this.sprite = augmentor.sprite;
        this.backgroundColor = augmentor.backgroundColor;
        this.triggerVal = augmentor.triggerVal;
        this.hasPrompt = augmentor.hasPrompt;
    }

    public virtual void UseAugment()
    {
        if(canActivate)
        {

        }
    }
    public virtual void PlayVFX()
    {
        //play animation or particle effects.
    }
    public virtual void PlayRandomBark()
    {
        //play random audio clip from barks.
    }
    public virtual void PlaySFX()
    {
        
    }
}
