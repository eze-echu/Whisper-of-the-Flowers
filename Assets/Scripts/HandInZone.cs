using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInZone : MonoBehaviour, IDropZone
{
    private delegate void handInActions();
    private handInActions handInAfter;
    private handInActions handInBefore;

    private PartycleController partycleController;

    public void Start()
    {
        partycleController = FindObjectOfType<PartycleController>();
    }

    public bool DropAction(GameObject a = null)
    {
        print("HandIN");
        gameObject.tag = "DropZone";
        handInBefore = delegate
        {
            a.GetComponent<Bouquet>().canBeDragged = false;

            partycleController.PlayParticle(a.GetComponent<Bouquet>().GetValues().message.ToString() == "Decrease_of_Love" ? 0 : 1);
            
        };
        handInAfter = delegate
        {
            partycleController.StopAllParticles();

            a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
            FindObjectOfType<FlowerHandler>()?.ResetWorkspace();
             a?.GetComponent<Bouquet>()?.ResetToOriginalState();
         };
        StartCoroutine(waitFewSeconds(3));
        return true;
    }

    private IEnumerator waitFewSeconds(float time)
    {
        handInBefore();
        yield return new WaitForSeconds(time);
        handInAfter();
    }
}
