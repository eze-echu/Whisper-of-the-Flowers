using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;
using Unity.VisualScripting;

// This one only manages the display and state of the Dialogue, things such as checking conditions and saving seen dialogues are handled by the DialogueHandler.cs
// Things such as current Dialogues/Chapters are managed by the story controller
// The state of the Game (End/reseting workspace/blocking interactions) will be handled by the GameStateManager
public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogueDisplay;
    private bool skiped = false;
    public float timeWrite;
    private bool isTyping = false;
    private Coroutine currentCoroutine;
    public Image clientImage;
    public Sprite[] clientSprites;
    public static DialogueController instance; 
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ShowSpecificRequest(Dialogue dialogue)
    {
        //StartCoroutine(TypeRequest(text));

        /*
        if (additionalRequest.Length > 0)
        {
            currentIndex = Random.Range(0, additionalRequest.Length);
            string selectedText = additionalRequest[currentIndex];
            StartCoroutine(TypeRequest(selectedText));


        }
        */
        if (isTyping)
        {
            StopCoroutine(currentCoroutine);
            isTyping = false;
        }

        currentCoroutine = StartCoroutine(TypeRequest(dialogue.dialogue[0], dialogue.portraitID));
    }

    private IEnumerator TypeRequest(string dialogue, int characterID)
    {

        isTyping = true;
        GameManager.Trigger("DisableWorkspace");
        setPortraitImage(characterID);
        dialogueDisplay.text = "";

        foreach (char c in dialogue)
        {
            if (skiped)
            {
                skiped = false;
                dialogueDisplay.text = dialogue;
                break;
            }
            dialogueDisplay.text += c;
            yield return new WaitForSeconds(timeWrite);
        }
        GameManager.Trigger("EnableWorkspace");
        GameManager.Trigger("CheckChapterEnd");
        isTyping = false;
    }
    public void skip()
    {
        if (isTyping)
        {
            skiped = true;
        }
    }
    private void setPortraitImage(int ID){
        if(clientSprites.Length > ID){
            clientImage.sprite = clientSprites[ID];
        }
        else{
            clientImage.sprite = clientSprites[0];
        }
    }
}
