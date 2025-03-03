using Flowers;
using Systems;
using UnityEngine;

namespace Racimo
{
    public partial class Bouquet
    {
        private delegate void handInActions();

        private handInActions handInAfter;
        private handInActions handInBefore;
        [SerializeField] private PartycleController partycleController;
        public AudioSource ParticleEffectSound;

        private void HandIn()
        {
            print("HandIN");
            gameObject.tag = "DropZone";
            handInBefore = delegate
            {
                //GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues...");
                GameManager.instance.AM.PlayEffect(ParticleEffectSound);
                GameManager.Trigger("DisableWorkspace");
                var grade = GameState.Instance.OrderSystem.CompleteOrder(this);
                MonoBehaviour.print("Grade: " + grade);
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
                ResetToOriginalState();
                GameState.Instance.NewRequest();
                GameState.ResumeGame();
            };
            StartCoroutine(WaitFewSeconds(3));
        }
    }
}