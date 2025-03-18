using System.Linq;
using Racimo.Vase;
using UnityEngine;

namespace Racimo.Decoration
{
    public class DecorationHandler: MonoBehaviour
    {
        public static DecorationHandler Instance;
        [SerializeField] public DecorationObject[] decorationScriptableObjects;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public DecorationType[] GetAvailableDecorationTypes()
        {
            return decorationScriptableObjects
                .Where(v => v.available)
                .Select(v => v.GetDecorationType())
                .ToArray();
        }

        public DecorationObject[] GetAvailableDecorationObjects()
        {
            return decorationScriptableObjects.Where(v => v.available).ToArray();
        }
    }

    public enum DecorationType
    {
        Card,
        Ribbon,
        Gem
    }
}