using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Racimo.Vase
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

}