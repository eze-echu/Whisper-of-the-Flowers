using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    private int _absoluteID;
    public int absoluteID{ // formateado como ChDiPo por ejemplo, capitulo 1, dialogo 3, posible 2 seria 01-03-02
        get{return _absoluteID; }
        set{
            _absoluteID = value;
            string absoluteIDString = _absoluteID.ToString();

            string chapterIDString = absoluteIDString.Substring(0, 2);
            _chapterID = int.Parse(chapterIDString);

            string dialogueIDString = absoluteIDString.Substring(2, 2);
            _dialogueID = int.Parse(dialogueIDString);

            string posibleDialogueIDString = absoluteIDString.Substring(4, 2);
            _possibleDialogueID = int.Parse(posibleDialogueIDString);
        }
    } 
    private int _chapterID; // = int.TryParse(absoluteID.ToString().Substring(0, 2));
    private int _dialogueID; // = int.TryParse(absoluteID.ToString().Substring(3, 2));
    private int _possibleDialogueID; //si es nulo o mayor a 0/1, significa que puede ser mas de uno si el dialogo lo muestra = int.TryParse(absoluteID.ToString().Substring(6, 2));
    public string[] dialogue;
    public requirements triggers;
    public bool ending;
    public int portraitID;

    // Este codigo checkea las condiciones cumplidas de un set de dialogos que comparten chapterID y dialogueID (comparten CH y DI, pero no PO)
    // una vez chequea las posibilidades, devuelve el valor que mas checks cumplio
    public static Dialogue CheckRequirements(Dialogue[] dialoguesToCheck, MessageType messageType, int intent, int formality, int[] dialoguesSeen = null)
    {
        Tuple<int, int> mostChecks = new Tuple<int, int>(0, 0);
        if(dialoguesToCheck.Length <= 0){
            return null;
        }
        foreach (var dialogue in dialoguesToCheck)
        {
            int checkList = 0;
            if (dialogue.triggers.intent > 0 ? dialogue.triggers.intent > intent : dialogue.triggers.intent < 0 && dialogue.triggers.intent < intent)
            {
                checkList++;
            }
            if (dialogue.triggers.formality > 0 ? dialogue.triggers.formality > formality : dialogue.triggers.formality < 0 && dialogue.triggers.formality < formality)
            {
                checkList++;
            }
            foreach(var dialogID in dialogue.triggers.mustSeeDialogWithID)
            {
                if (dialoguesSeen.Contains(dialogID))
                {
                    checkList++;
                }
            }
            if (checkList > mostChecks.Item2) {
                mostChecks = new Tuple<int, int>(dialogue.absoluteID, checkList);
            }
        }
        return dialoguesToCheck.Where(x => x.absoluteID == mostChecks.Item1).First();
    }
}
public struct requirements
{
    public int intent;
    public int formality;
    public MessageType message;
    public int[] mustSeeDialogWithID; //absolute IDs that must be seen, obtained from SAVE

}