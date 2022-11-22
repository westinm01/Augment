using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augmentor : MonoBehaviour
{
    public string name;
    public Sprite sprite;
    public Color backgroundColor;
    public ChessPiece augmentPiece;
    public bool canActivate = false;
    public string augmentName;
    public string augmentDesc;
    public int triggerVal;
    public List<AudioClip> barks = new List<AudioClip>();
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
