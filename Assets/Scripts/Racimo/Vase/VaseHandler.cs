﻿using System.Linq;
using UnityEngine;

namespace Racimo.Vase
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

        public VaseObject[] GetAvailableVaseObjects()
        {
            return vaseScriptableObjects.Where(v => v.available).ToArray();
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