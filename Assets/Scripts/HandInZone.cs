using System.Collections;
using System.Collections.Generic;
using Flowers;
using Systems;
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
        if(a != null && a.name == "Racimo"){
            print("HandIN");
            gameObject.tag = "DropZone";
            if (a.GetComponent<Bouquet>() is not { } b)
            {
                throw new System.Exception("Bouquet component not found in Object 'Racimo', make sure has the script bouquet");
            }
            handInBefore = delegate
            {
                //GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues...");
                GameManager.instance.AM.PlayEffect(EffectSound);
                GameManager.Trigger("DisableWorkspace");
                b.canBeDragged = false;
                var message = b.GetValues().message.ToString();
                var intent = b.GetValues().intent;
                var grade = GameState.Instance.OrderSystem.GradeBouquet(b);
                print("Grade: " + grade);
                if (grade == 0)
                {
                    partycleController.PlayParticle(2);
                }
                else if (Mathf.Approximately(grade, 1f))
                {
                    partycleController.PlayParticle(3);
                }
                else
                {
                    partycleController.PlayParticle(0);
                }
                GameState.Instance.AddRequestReward(Mathf.Clamp(grade, 0.3f, 1f));
                // if (intent == 5 && message == "Love")
                // {
                //     partycleController.PlayParticle(3);
                // }
                // else if (intent == -5 && message == "Hatred")
                // {
                //     partycleController.PlayParticle(2);
                // }
                // else
                // {
                //     partycleController.PlayParticle(message == "Decrease_of_Love" || message == "Jealousy" || message == "Mourning" || message == "Hatred" ? 0 : 1);
                // }
                GameState.PauseGame();
                // StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));

            };
            handInAfter = delegate
            {
                partycleController.StopAllParticles();
                //StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));
                // a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
                FlowerHandler.instance.ResetWorkspace();
                b.ResetToOriginalState();
                GameState.Instance.NewRequest();
                GameState.ResumeGame();
            };
            StartCoroutine(WaitFewSeconds(3));
            return true;
        }
        else{
            return false;
        }
    }

    private IEnumerator WaitFewSeconds(float time)
    {
        handInBefore();
        yield return new WaitForSeconds(time);
        handInAfter();
    }
}
