using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Linq;
using UnityEditor.Timeline;
using Unity.VisualScripting;

public class DialogueHandler : MonoBehaviour
{
    private Dialogue[] _dialogues;

    public Dialogue[] dialogues{get{ return _dialogues;} set{_dialogues=value;}}
    public DialogueHandler(Dialogue[] dialoguesToUse){
        dialogues = dialoguesToUse;
    }
    public Dialogue BringNextDialogue(int currentID, MessageType message, int intent, int formality){
        int[] TODO = new int[1]{0}; //Queda implementar los guardados
        return Dialogue.CheckRequirements(_dialogues.Where(id => id.absoluteID.ToString().StartsWith(currentID.ToString().Substring(0,4))).ToArray(), message, intent, formality, null);
    }
}
