using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryButton : MonoBehaviour
{
    private Button button;
    public List<int> dialogueOrder = new List<int>();
    public List<int> speakers = new List<int>();
    public DialogueSystem dialogueSystem;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        // Add your custom button behavior here
        Debug.Log("Story Button clicked!");
        dialogueSystem.SetDialogueOrder(dialogueOrder);
        dialogueSystem.SetCurrentSpeakers(speakers);
    }
}
