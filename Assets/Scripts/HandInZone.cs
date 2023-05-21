using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInZone : MonoBehaviour, IDropZone
{
    private delegate void handInActions();
    private handInActions handInAfter;
    private handInActions handInBefore;
    public bool DropAction(GameObject a = null)
    {
        print("HandIN");
        gameObject.tag = "DropZone";
        handInBefore = delegate
        {
            a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
        };
        handInAfter = delegate
         {
             FindObjectOfType<FlowerHandler>()?.ResetWorkspace();
             a?.GetComponent<Bouquet>()?.ResetToOriginalState();
         };
        waitFewSeconds(3);
        return true;
    }

    private IEnumerable waitFewSeconds(float time)
    {
        handInBefore();
        yield return new WaitForSeconds(time);
        handInAfter();
    }
}
