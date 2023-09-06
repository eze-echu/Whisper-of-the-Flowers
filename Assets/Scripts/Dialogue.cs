using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    public int absoluteID; // formateado como CH-DI-PO por ejemplo, capitulo 1, dialogo 3, posible 2 seria 01-03-02
    int chapterID; // = int.TryParse(absoluteID.ToString().Substring(0, 2));
    int dialogueID; // = int.TryParse(absoluteID.ToString().Substring(3, 2));
    public int possibleDialogueID; //si es nulo o mayor a 0/1, significa que puede ser mas de uno si el dialogo lo muestra = int.TryParse(absoluteID.ToString().Substring(6, 2));
    string[] dialogue;
    public requirements triggers;
    bool ending;

    // Este codigo checkea las condiciones cumplidas de un set de dialogos que comparten chapterID y dialogueID (comparten CH y DI, pero no PO)
    // una vez chequea las posibilidades, devuelve el valor que mas checks cumplio
    public Dialogue CheckRequirements(Dialogue[] dialoguesToCheck, int intent, int formality, int[] dialoguesSeen)
    {
        Tuple<int, int> mostChecks = new Tuple<int, int>(0, 0);
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
    public int[] mustSeeDialogWithID;

}