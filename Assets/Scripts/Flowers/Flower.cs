using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flower : ScriptableObject
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
    public MeshRenderer renderer;
    public bool available;
    public MeshFilter model;
}
