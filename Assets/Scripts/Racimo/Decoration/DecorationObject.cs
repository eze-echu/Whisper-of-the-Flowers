using System;
using UnityEngine;
using Racimo.Decoration;
using UnityEngine.Serialization;

namespace Racimo.Decoration
{
    [CreateAssetMenu]
    public class DecorationObject: ScriptableObject
    {
        public bool available;
        [SerializeField] public DecorationModel decorationModel;
        [SerializeField] private DecorationType decorationType;

        internal DecorationType GetDecorationType()
        {
            return decorationType;
        }
    }

    [Serializable]
    public struct DecorationModel
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;

        private bool IsValid()
        {
            return meshFilter != null && meshRenderer != null;
        }

        public DecorationModel(MeshFilter filter, MeshRenderer renderer)
        {
            meshFilter = filter;
            meshRenderer = renderer;
            if (!IsValid())
            {
                Debug.LogError("Invalid DecorationModel: MeshFilter or MeshRenderer is null.");
            }
        }

        public MeshFilter GetMeshFilter()
        {
            return meshFilter;
        }
        public MeshRenderer GetMeshRenderer()
        {
            return meshRenderer;
        }
    }
}