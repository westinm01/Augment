using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Augmentor", menuName = "New Augmentor")]
public class AugmentorObject : ScriptableObject
{
    public string characterName;
    public string augmentName;
    public string description;
    public Sprite sprite;
    public Color backgroundColor;
    public int triggerVal;
    public bool hasPrompt;
}
