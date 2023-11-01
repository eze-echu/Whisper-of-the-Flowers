using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    private string _absoluteID;
    public string absoluteID{ // formateado como ChDiPo por ejemplo, capitulo 1, dialogo 3, posible 2 seria 01-03-02
        get{return _absoluteID; }
        set{
            _absoluteID = value;

            _chapterID = _absoluteID.Substring(0, 2);

            _dialogueID = _absoluteID.Substring(2, 2);

            possibleDialogueID = _absoluteID.Substring(4, 2);
        }
    }
    public string Comment; //Only exists to make it easier to identify dialogues and duplicates
    private string _chapterID; // = int.TryParse(absoluteID.ToString().Substring(0, 2));
    private string _dialogueID; // = int.TryParse(absoluteID.ToString().Substring(3, 2));
    public string possibleDialogueID; //si es nulo o mayor a 0/1, significa que puede ser mas de uno si el dialogo lo muestra = int.TryParse(absoluteID.ToString().Substring(6, 2));
    public string[] dialogue;
    [SerializeField]
    private requirements _triggers;
    public bool ending;
    public int portraitID;
    public Flower flower;

    // Este codigo checkea las condiciones cumplidas de un set de dialogos que comparten chapterID y dialogueID (comparten CH y DI, pero no PO)
    // una vez chequea las posibilidades, devuelve el valor que mas checks cumplio
    public static Dialogue CheckRequirements(Dialogue[] dialoguesToCheck, FlowerMessageType messageType = FlowerMessageType.Null, int intent = 0, int formality = 0, string[] dialoguesSeen = null)
    {
        if(dialoguesToCheck.Length <= 0){
            return null;
        }
        Tuple<string, int> mostChecks = new Tuple<string, int>(dialoguesToCheck[0].absoluteID, 0);
        //Debug.LogWarning(intent.ToString() + formality.ToString());
        foreach (var dialogue in dialoguesToCheck)
        {
            int checkList = 0;
            // if (dialogue._triggers.intent > 0 ? dialogue._triggers.intent > intent : dialogue._triggers.intent < 0 && dialogue._triggers.intent < intent)
            // {
            //     checkList++;
            // }
            if(dialogue._triggers.intent > 0 && dialogue._triggers.intent < intent){
                checkList++;
            }
            else if(dialogue._triggers.intent < 0 && dialogue._triggers.intent > intent){
                checkList++;
            }
            // if (dialogue._triggers.formality > 0 ? dialogue._triggers.formality > formality : dialogue._triggers.formality < 0 && dialogue._triggers.formality < formality)
            // {
            //     checkList++;
            // }
            if(dialogue._triggers.formality > 0 && dialogue._triggers.formality < formality){
                checkList++;
            }
            else if(dialogue._triggers.formality < 0 && dialogue._triggers.formality > formality){
                checkList++;
            }
            if(dialoguesSeen != null){
                foreach(var dialogID in dialogue._triggers.mustSeeDialogWithID)
                {
                    if (dialoguesSeen.Contains(dialogID))
                    {
                        checkList++;
                    }
                }
            }
            if(dialogue._triggers.messageInclude.Contains(messageType)){
                checkList++;
            } else
            if(dialogue._triggers.messageExclude.Contains(messageType)){
                checkList = 0;
            }
            if (checkList == mostChecks.Item2 && dialogue._triggers.messageInclude.Contains(messageType)) {
                mostChecks = new Tuple<string, int>(dialogue.absoluteID, checkList);
            }
            else if(checkList > mostChecks.Item2){
                mostChecks = new Tuple<string, int>(dialogue.absoluteID, checkList);
            }
        }
        return dialoguesToCheck.Where(x => x.absoluteID == mostChecks.Item1).First();
    }
    public static Dialogue CheckRequirements(Dialogue[] dialoguesToCheck, string[] dialoguesSeen = null)
    {
        if(dialoguesToCheck.Length <= 0){
            Debug.LogError("No dialogues reached the checkingStage");
            return null;
        }
        Tuple<string, int> mostChecks = new Tuple<string, int>(dialoguesToCheck[0].absoluteID, 0);
        foreach (var dialogue in dialoguesToCheck)
        {
            int checkList = 0;
            if(dialoguesSeen != null){
                foreach(var dialogID in dialogue._triggers.mustSeeDialogWithID)
                {
                    if (dialoguesSeen.Length > 0 && dialoguesSeen.Contains(dialogID))
                    {
                        checkList++;
                    }
                }
            }
            if (checkList > mostChecks.Item2) {
                mostChecks = new Tuple<string, int>(dialogue.absoluteID, checkList);
            }
        }
        return dialoguesToCheck.Where(x => x.absoluteID == mostChecks.Item1).First();
    }
}
[System.Serializable]
public struct requirements
{
    public int intent;
    public int formality;
    public FlowerMessageType[] messageInclude;
    public FlowerMessageType[] messageExclude;
    public string[] mustSeeDialogWithID; //absolute IDs that must be seen, obtained from SAVE

}