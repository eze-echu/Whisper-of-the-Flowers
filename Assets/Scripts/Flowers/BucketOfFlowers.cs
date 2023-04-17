using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketOfFlowers : MonoBehaviour
{
    private MeshFilter[] displayFlowers;
    private MeshRenderer[] renderers;
    public Flower flower { get => flower; set => SetFlower(value); }
    private void Awake()
    {
        displayFlowers = GetComponentsInChildren<MeshFilter>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        /*foreach (var a in displayFlowers)
        {
            if (a.gameObject != gameObject)
            {
                print(a.gameObject.name);
                renderers.Add(a.GetComponent<MeshRenderer>());
            }
        }*/
    }

    private void SetFlower(Flower f)
    {
        for (int i = 0; i < displayFlowers.Length; i++)
        {
            if (displayFlowers[i].gameObject.name != gameObject.name)
            {
                renderers[i].material = f.renderer.sharedMaterial;
                displayFlowers[i].mesh = f.model.sharedMesh;
            }
        }
    }
}
