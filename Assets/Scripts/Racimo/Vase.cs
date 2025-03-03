using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Racimo
{
    [System.Serializable]
    public class Vase
    {
        [SerializeField] private VaseObject currentVaseObject;
        private List<MeshRenderer> _currentVaseModel;
        private List<MeshFilter> _currentVaseFilter;

        public void UpdateModel()
        {
            if (currentVaseObject == null) return;
            _currentVaseModel.ForEach(m =>
                m.sharedMaterials = currentVaseObject.vaseModel.GetMeshRenderer().sharedMaterials);
            _currentVaseFilter.ForEach(
                action: m => m.sharedMesh = currentVaseObject.vaseModel.GetMeshFilter().sharedMesh);
        }

        public void SetVase(VaseObject vaseObject)
        {
            currentVaseObject = vaseObject;
            UpdateModel();
        }

        public VaseObject GetVase()
        {
            return currentVaseObject;
        }

        public Vase(List<MeshFilter> vaseFilter, [ItemCanBeNull] List<MeshRenderer> vaseModel, VaseObject vaseObject)
        {
            _currentVaseModel = vaseModel;
            _currentVaseFilter = vaseFilter;
            currentVaseObject = vaseObject;
            UpdateModel();
        }
    }
    public partial class Bouquet
    {
        public void NextVase()
        {
            if (_currentWorkstations != Workstations.VaseStation)
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

            var currentVaseIndex = System.Array.IndexOf(availableVaseObjects, vase.GetVase());

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
            if (_currentWorkstations != Workstations.VaseStation)
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

            var currentVaseIndex = System.Array.IndexOf(availableVaseObjects, vase.GetVase());

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