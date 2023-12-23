using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInZone : MonoBehaviour, IDropZone
{
    private delegate void handInActions();
    private handInActions handInAfter;
    private handInActions handInBefore;

    private PartycleController partycleController;
    public AudioSource EffectSound;

    public void Start()
    {
        partycleController = FindObjectOfType<PartycleController>();
    }

    public bool DropAction(GameObject a = null)
    {
        if(a.name == "Racimo"){
            print("HandIN");
            gameObject.tag = "DropZone";
            handInBefore = delegate
            {
                //GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues...");
                GameManager.instance.AM.PlayEffect(EffectSound);
                GameManager.Trigger("DisableWorkspace");
                a.GetComponent<Bouquet>().canBeDragged = false;
                string intent = a.GetComponent<Bouquet>().GetValues().message.ToString();
                if (a.GetComponent<Bouquet>().GetValues().intent == 5 && intent == "Love")
                {
                    partycleController.PlayParticle(3);
                }
                else if (a.GetComponent<Bouquet>().GetValues().intent == -5 && intent == "Hatred")
                {
                    partycleController.PlayParticle(2);
                }
                else
                {
                    partycleController.PlayParticle(intent == "Decrease_of_Love" || intent == "Jealousy" || intent == "Mourning" || intent == "Hatred" ? 0 : 1);
                }
                StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));

            };
            handInAfter = delegate
            {
                partycleController.StopAllParticles();
                //StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));
                a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
                FindObjectOfType<FlowerHandler>()?.ResetWorkspace();
                a?.GetComponent<Bouquet>()?.ResetToOriginalState();
            };
            StartCoroutine(waitFewSeconds(3));
            return true;
        }
        else{
            return false;
        }
    }

    private IEnumerator waitFewSeconds(float time)
    {
        handInBefore();
        yield return new WaitForSeconds(time);
        handInAfter();
    }
}
