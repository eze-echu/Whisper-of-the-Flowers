using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using System.Runtime.CompilerServices;
using System;

public class StoryController : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    public dialoguePerChapter[] dialoguePerChapters;
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
        dialogueHandler = DialogueHandler.instance;
        if(dialoguePerChapters.Length > 0){
            foreach (var c in dialoguePerChapters)
            {
                foreach(var d in c.dialogues){
                    dialogueHandler.dialogues.Add(d);
                    d.absoluteID = d.name; //this is so that the separate identifiers inside the dialogue SO are set properly
                }
            }
        }
        else{
            Debug.LogError("Missing Dialogues in the StoryController, are there any dialogue SOs in the controller?");
        }
        StartStory();
        GameManager.Subscribe("CheckChapterEnd", CheckChapterEnd);
        GameManager.Subscribe("StartStory", StartStory);
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("CheckChapterEnd", CheckChapterEnd);
        GameManager.Unsuscribe("StartStory", StartStory);
    }

    public void ProgressStory(FlowerMessageType messageType, int intent, int formality){
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
            ChapterEnd();
        }
        else{
            GameManager.Trigger("EnableWorkspace");
        }
    }
    private void ChapterEnd(){
        GameManager.Trigger("EndChapter");
        GameStateManager.instance?.EndMessage.SetText(dialogueHandler.GetSpecificDialogue(currentChapter,currentDialogue,currentPossible).dialogue[1]);
        currentChapter++;
        currentDialogue = 0;
        currentPossible = 0;
        bool nextChapterHasDialogue = dialogueHandler.GetDialoguePossibilities(currentChapter, currentDialogue).Length > 0 ? true : false;
        //TODO: Save Current Chapter

    }
    public Dialogue BringCurrentDialogue(){
        if(DialogueHandler.instance.GetSpecificDialogue(currentChapter, currentDialogue, currentPossible)){
            return DialogueHandler.instance.GetSpecificDialogue(currentChapter, currentDialogue, currentPossible);
        }
        else{
            return null;
        }
    }
[Serializable]
public struct dialoguePerChapter{
    [SerializeField]
    public Dialogue[] dialogues;
}
}
