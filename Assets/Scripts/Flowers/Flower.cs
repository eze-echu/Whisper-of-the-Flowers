using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flower : ScriptableObject
{
    public bool available;
    public FlowerValues flowerValues;
    public FlowerModel flowerModel;
}
[System.Serializable]
public struct FlowerModel{
    public MeshFilter model;
    public MeshRenderer materials;
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
}
