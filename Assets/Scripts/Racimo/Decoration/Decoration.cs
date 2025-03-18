using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Racimo.Decoration
{
    [Serializable]
    public class Decoration
    {
        [SerializeField] private DecorationObject currentDecorationObject;
        private List<MeshRenderer> _currentDecorationModel;
        private List<MeshFilter> _currentDecorationFilter;

        public void UpdateModel()
        {
            if (currentDecorationObject == null) return;
            _currentDecorationModel.ForEach(m =>
                m.sharedMaterials = currentDecorationObject.decorationModel.GetMeshRenderer().sharedMaterials);
            _currentDecorationFilter.ForEach(
                action: m => m.sharedMesh = currentDecorationObject.decorationModel.GetMeshFilter().sharedMesh);
        }

        public void SetDecoration(DecorationObject decorationObject)
        {
            currentDecorationObject = decorationObject;
            UpdateModel();
        }

        public DecorationObject GetDecoration()
        {
            return currentDecorationObject;
        }

        public Decoration(List<MeshFilter> decorationFilter, [ItemCanBeNull] List<MeshRenderer> decorationModel, DecorationObject decorationObject)
        {
            _currentDecorationModel = decorationModel;
            _currentDecorationFilter = decorationFilter;
            currentDecorationObject = decorationObject;
            UpdateModel();
        }
    }
}