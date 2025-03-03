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
        private List<MeshRenderer> currentVaseModel;
        private List<MeshFilter> currentVaseFilter;

        public void UpdateModel()
        {
            if (currentVaseObject == null) return;
            currentVaseModel.ForEach(m =>
                m.sharedMaterials = currentVaseObject.vaseModel.GetMeshRenderer().sharedMaterials);
            currentVaseFilter.ForEach(
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
            currentVaseModel = vaseModel;
            currentVaseFilter = vaseFilter;
            currentVaseObject = vaseObject;
            UpdateModel();
        }
    }
}