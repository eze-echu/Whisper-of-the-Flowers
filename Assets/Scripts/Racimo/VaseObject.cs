using UnityEngine;

namespace Racimo
{

    [CreateAssetMenu]
    public class VaseObject : ScriptableObject
    {
        public bool available;
        [SerializeField] public VaseModel vaseModel;
        [SerializeField] private VaseType vaseType;

        internal VaseType GetVaseType()
        {
            return vaseType;
        }
    }

    [System.Serializable]
    public class VaseModel
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;

        private bool IsValid()
        {
            return meshFilter != null && meshRenderer != null;
        }

        public VaseModel()
        {
            meshFilter = null;
            meshRenderer = null;
        }

        public VaseModel(MeshFilter otherFilter, MeshRenderer otherRenderer)
        {
            meshFilter = otherFilter;
            meshRenderer = otherRenderer;
            if (!IsValid())
            {
                Debug.LogError("Invalid VaseModel: MeshFilter or MeshRenderer is null.");
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