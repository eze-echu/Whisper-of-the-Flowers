using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInZone : MonoBehaviour, IDropZone
{
    public bool DropAction(GameObject a = null)
    {
        print("HandIN");
        gameObject.tag = "DropZone";
        a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
        FindObjectOfType<FlowerHandler>()?.ResetWorkspace();
        a?.GetComponent<Bouquet>()?.ResetToOriginalState();
        return true;
    }
}
