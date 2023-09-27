using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Linq;
using Unity.VisualScripting;

public class DialogueHandler : MonoBehaviour
{
    private List<Dialogue> _dialogues;

    public List<Dialogue> dialogues{get{ return _dialogues;} set{_dialogues=value;}}
    public static DialogueHandler instance; 
    public void Awake()
    {
        if (instance == null){ instance = this; _dialogues = new List<Dialogue>();}
        else Destroy(gameObject);
    }
    public DialogueHandler(Dialogue dialoguesToUse){
        dialogues.Add(dialoguesToUse);
    }
    public Dialogue BringNextDialogue(Dialogue[] dialoguesSeparated, FlowerMessageType message, int intent, int formality, string[] seenDialogues = null){
        //Queda implementar los guardados
        return Dialogue.CheckRequirements(dialoguesSeparated, message, intent, formality, seenDialogues);
    }
    public Dialogue BringNextDialogue(Dialogue[] dialoguesSeparated, string[] seenDialogues = null){
        return Dialogue.CheckRequirements(dialoguesSeparated, seenDialogues);
    }
    public Dialogue[] GetDialoguePossibilities(int chapterID, int dialogID){
        string CH = chapterID.ToString();
        string DI = dialogID.ToString();
        if(CH.Length < 2){
            CH = 0 + CH;
        }
        if(DI.Length < 2){
            DI = 0 + DI;
        }
        return _dialogues.Where(id => id.absoluteID.StartsWith(CH + DI)).ToArray();
    }
    public Dialogue GetSpecificDialogue(int chapterID, int dialogID, int possibleID){
        string CH = chapterID.ToString();
        string DI = dialogID.ToString();
        string PO = possibleID.ToString();
        if(CH.Length < 2){
            CH = 0 + CH;
        }
        if(DI.Length < 2){
            DI = 0 + DI;
        }
        if(PO.Length < 2){
            PO = 0 + PO;
        }
        return _dialogues.Where(id => id.absoluteID.StartsWith(CH + DI + PO))?.First() ? _dialogues.Where(id => id.absoluteID.StartsWith(CH + DI + PO)).First() : null;
    }
}
