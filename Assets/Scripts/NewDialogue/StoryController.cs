using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class StoryController : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    public Dialogue[] dialogues;
    public int currentChapter;
    public int currentDialogue;
    public int currentPossible;
    public static StoryController instance; 
    private List<string> testSeen = new List<string>();
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start(){
        foreach (var d in dialogues)
        {
            d.absoluteID = d.absoluteID; //this is so that the separate identifiers inside the dialogue SO are set properly
        }
        dialogueHandler = DialogueHandler.instance;
        if(dialogues.Length > 0){
            dialogueHandler.dialogues = dialogues;
        }
        else{
            Debug.LogError("Missing Dialogues in the StoryController, are there any dialogue SOs in the controller?");
        }
        StartStory();
        GameManager.Subscribe("CheckChapterEnd", CheckChapterEnd);
    }

    public void ProgressStory(MessageType messageType, int intent, int formality){
        currentDialogue++;
        //print(dialogueHandler.GetDialoguePossibilities(currentChapter, currentDialogue).Length);
        Dialogue nextDialogue = dialogueHandler.BringNextDialogue(dialogueHandler.GetDialoguePossibilities(currentChapter, currentDialogue), messageType, intent, formality);
        if(nextDialogue == null){
            Debug.LogError("Missing Dialogue");
        }
        else{
            DialogueController.instance.ShowSpecificRequest(nextDialogue);
            currentPossible = int.Parse(nextDialogue.possibleDialogueID);
            testSeen.Add(nextDialogue.absoluteID);
        }
    }
    public void StartStory(){
        Dialogue nextDialogue = dialogueHandler.BringNextDialogue(dialogueHandler.GetDialoguePossibilities(currentChapter, currentDialogue));
        if(nextDialogue == null){
            Debug.LogError("Missing Dialogue");
        }
        else{
            DialogueController.instance.ShowSpecificRequest(nextDialogue);
            print(nextDialogue.possibleDialogueID);
            currentPossible = int.Parse(nextDialogue.possibleDialogueID);
            testSeen.Add(nextDialogue.absoluteID);
        }
    }
    private void CheckChapterEnd(){
        if(dialogueHandler.GetSpecificDialogue(currentChapter,currentDialogue,currentPossible).ending){
                GameManager.Trigger("EndChapter");
                GameStateManager.instance?.EndMessage.SetText(dialogueHandler.GetSpecificDialogue(currentChapter,currentDialogue,currentPossible).dialogue[1]);
                currentChapter++;
        }
    }
}
