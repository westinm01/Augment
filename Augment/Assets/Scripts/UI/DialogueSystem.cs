using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class DialogueSystem : MonoBehaviour
{
    public List<Image> augmentorProfileImages = new List<Image>();
    public List<int> dialogueOrder = new List<int>();
    public List<int> currentSpeakers = new List<int>();
    private int index = 0;

    public int chapter = 1;

    public DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Called at the beginning of clicking a level. Will set the order of dialogue.
    public void SetDialogueOrder(List<int> newDialogueOrder)
    {
        dialogueOrder.Clear();
        for(int i = 0; i < newDialogueOrder.Count; i++)
        {
            dialogueOrder.Add(newDialogueOrder[i]);
        }
    }

    public void SetCurrentSpeakers(List<int> augmentorNums)
    {
        currentSpeakers.Clear();
        for(int i = 0; i < augmentorNums.Count; i++)
        {
            currentSpeakers.Add(augmentorNums[i]);
            //currentSpeakers.Add(augmentorProfileImages[augmentorNums[i]]);
        }
        DisplaySpeakers();
    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }

    public void IncrementIndex()
    {
        index++;
        if (index > dialogueOrder.Count)
        {
            //dialogue completed.
        }
        else
        {
            UpdateSpeaker(); 
        }
        
    }

    public void DisplaySpeakers()
    {
        for(int i = 0; i < currentSpeakers.Count; i++)
        {
            Debug.Log("CS: " + i);
            this.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = augmentorProfileImages[currentSpeakers[i]].sprite;
        }
        //need to hide white square sprites if there are less than 4 speakers.

    }

    public void UpdateSpeaker()
    {
        Debug.Log(dialogueOrder[index]);
        for(int i = 0; i < currentSpeakers.Count; i++)
        {
            if(i != dialogueOrder[index] - 1)
            {
                this.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                this.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void ClearDialogueMenu()
    {
        currentSpeakers.Clear();
        dialogueOrder.Clear();
        index = 0;
    }



    public void BeginDialogue()
    {
        dialogueRunner.startNode = "Chapter" + chapter;
        //dialogueRunner.StartDialogue(yp.sourceScripts[1]);
        //dialogueRunner.yarnProject = yp;
        /*
        YarnProject yarnProject = dialogueRunner.yarnProject;
        YarnScripts yarnScripts = yarnProject.GetScripts();
        YarnScript fifthScript = yarnScripts.GetScriptAt(1);
        dialogueRunner.StartDialogue(fifthScript);
        */
    }
    

}
