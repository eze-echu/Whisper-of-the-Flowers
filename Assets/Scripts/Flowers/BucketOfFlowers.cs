using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketOfFlowers : MonoBehaviour
{
    public GameObject flowerFather;
    private MeshFilter[] displayFlowers;
    private MeshRenderer[] renderers;
    public Flower flower { get => flower; set => SetFlower(value); }

    public void Move(Vector3 position)
    {
        foreach(var a in displayFlowers)
        {
            a.transform.position = position;
        }
    }

    private void Awake()
    {
        displayFlowers = flowerFather.GetComponentsInChildren<MeshFilter>();
        renderers = flowerFather.GetComponentsInChildren<MeshRenderer>();
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
                renderers[i].material = f.flowerModel.materials.sharedMaterial;
                displayFlowers[i].mesh = f.flowerModel.model.sharedMesh;
            }
        }
    }
}
