using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Racimo
{
    public class VaseHandler: MonoBehaviour
    {
        public static VaseHandler Instance;
        [SerializeField] public VaseObject[] vaseScriptableObjects;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public VaseType[] GetAvailableVaseTypes()
        {
            return vaseScriptableObjects
                .Where(v => v.available)
                .Select(v => v.GetVaseType())
                .ToArray();
        }
    }

    public enum VaseType
    {
        Ceramic,
        Glass,
        Metal,
        Paper
    }
}