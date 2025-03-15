using Racimo.Vase;
using UnityEngine;

namespace Racimo
{
    public partial class Bouquet
    {
        public VaseType GetVaseType()
        {
            return vase.GetVase().GetVaseType();
        }

        public void NextVase()
        {
            if (_currentWorkstations != Racimo.Bouquet.Workstations.VaseStation)
            {
                Debug.LogWarning("Not in vase station");
                return;
            }

            var availableVaseObjects = VaseHandler.Instance.GetAvailableVaseObjects();

            if (availableVaseObjects.Length == 0)
            {
                Debug.LogError("No available vases, Check VaseHandler");
                return;
            }

            var currentVaseIndex = System.Array.IndexOf<VaseObject>(availableVaseObjects, vase.GetVase());

            if (currentVaseIndex == -1)
            {
                Debug.LogError("Current vase not found in available vases");
                return;
            }

            var nextVaseIndex = (currentVaseIndex + 1) % availableVaseObjects.Length;

            vase.SetVase(availableVaseObjects[nextVaseIndex]);
        }

        public void PreviousVase()
        {
            if (_currentWorkstations != Racimo.Bouquet.Workstations.VaseStation)
            {
                Debug.LogWarning("Not in vase station");
                return;
            }

            var availableVaseObjects = VaseHandler.Instance.GetAvailableVaseObjects();

            if (availableVaseObjects.Length == 0)
            {
                Debug.LogError("No available vases, Check VaseHandler");
                return;
            }

            var currentVaseIndex = System.Array.IndexOf<VaseObject>(availableVaseObjects, vase.GetVase());

            if (currentVaseIndex == -1)
            {
                Debug.LogError("Current vase not found in available vases");
                return;
            }

            var desired = currentVaseIndex - 1;
            if (desired < 0)
            {
                desired = availableVaseObjects.Length - 1;
            }

            var nextVaseIndex = (desired) % availableVaseObjects.Length;

            vase.SetVase(availableVaseObjects[nextVaseIndex]);
        }
    }
}