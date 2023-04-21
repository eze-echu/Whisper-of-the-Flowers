using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flower : ScriptableObject
{
    public bool available;
    public FlowerValues flowerValues;
    public FlowerModel flowerModel;
    public Flower()
    {
        flowerModel = new FlowerModel
        {
            flowerMesh = null,
            flowerMaterial = null
        };
    }
}
[System.Serializable]
public struct FlowerModel{
    public MeshFilter flowerMesh;
    public MeshRenderer flowerMaterial;
    public bool IsValid()
    {
        return flowerMesh != null && flowerMaterial != null;
    }
}
[System.Serializable]
public struct FlowerValues
{
    public MessageType message;
    [Range(-5, 5)]
    public int intent;
    [Range(-5, 5)]
    public int formality;
    [Range(-3, 3)]
    public int intentMultiplier;
    [Range(-3, 3)]
    public int formalityMultiplier;
    public void Validate()
    {
        intent = Mathf.Clamp(intent, -5, 5);
        formality = Mathf.Clamp(formality, -5, 5);
        intentMultiplier = Mathf.Clamp(intentMultiplier, -3, 3);
        formalityMultiplier = Mathf.Clamp(formalityMultiplier, -3, 3);
    }
}

