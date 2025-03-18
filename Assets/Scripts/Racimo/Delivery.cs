using Flowers;
using Racimo.Decoration;
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
                    partycleController.PlayParticle(2); // Broken Hearts
                }
                else if (grade >= 0.75f)
                {
                    partycleController.PlayParticle(3); // Plus Hearts
                }
                else
                {
                    partycleController.PlayParticle(0); // Tears/Sweat drops
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
                decorationObject.SetActive(false);
                //StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));
                // a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
                FlowerHandler.instance.ResetWorkspace();
                ResetToOriginalState();
                GameState.Instance.NewRequest();
                GameState.ResumeGame();
            };
            StartCoroutine(WaitFewSeconds(3));
        }

        public DecorationType GetDecorationType()
        {
            return decoration.GetDecoration().GetDecorationType();
        }

        public void NextDecoration()
        {
            if (_currentWorkstations != Racimo.Bouquet.Workstations.DeliveryStation)
            {
                Debug.LogWarning("Not in decoration station");
                return;
            }

            var availableDecorationObjects = DecorationHandler.Instance.GetAvailableDecorationObjects();

            if (availableDecorationObjects.Length == 0)
            {
                Debug.LogError("No available decorations, Check DecorationHandler");
                return;
            }

            var currentDecorationIndex = System.Array.IndexOf<DecorationObject>(availableDecorationObjects, decoration.GetDecoration());

            if (currentDecorationIndex == -1)
            {
                Debug.LogError("Current decoration not found in available decorations");
                return;
            }

            var nextDecorationIndex = (currentDecorationIndex + 1) % availableDecorationObjects.Length;

            decoration.SetDecoration(availableDecorationObjects[nextDecorationIndex]);
        }

        public void PreviousDecoration()
        {
            if (_currentWorkstations != Racimo.Bouquet.Workstations.DeliveryStation)
            {
                Debug.LogWarning("Not in decoration station");
                return;
            }

            var availableDecorationObjects = DecorationHandler.Instance.GetAvailableDecorationObjects();

            if (availableDecorationObjects.Length == 0)
            {
                Debug.LogError("No available decorations, Check DecorationHandler");
                return;
            }

            var currentDecorationIndex = System.Array.IndexOf<DecorationObject>(availableDecorationObjects, decoration.GetDecoration());

            if (currentDecorationIndex == -1)
            {
                Debug.LogError("Current decoration not found in available decorations");
                return;
            }

            var desired = currentDecorationIndex - 1;
            if (desired < 0)
            {
                desired = availableDecorationObjects.Length - 1;
            }

            var nextDecorationIndex = (desired) % availableDecorationObjects.Length;

            decoration.SetDecoration(availableDecorationObjects[nextDecorationIndex]);
        }
    }
}