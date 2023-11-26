using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketOfFlowers : MonoBehaviour, IResteable, IDropZone, IOccupied
{
    public GameObject flowerFather;
    private FlowerBunch flowerBunch;
    public Vector3 OGflowerPosition;
    private MeshFilter[] displayFlowers;
    private MeshRenderer[] renderers;
    public Flower flower { get => _flower; set => SetFlower(value); }
    private Flower _flower;

    public void ResetToOriginalState()
    {
        tag = "Occupied";
        flowerBunch.ResetToOriginalState();
    }

    private void Awake()
    {
        displayFlowers = flowerFather.GetComponentsInChildren<MeshFilter>();
        renderers = flowerFather.GetComponentsInChildren<MeshRenderer>();
        flowerBunch = GetComponentInChildren<FlowerBunch>();
        OGflowerPosition = flowerBunch.transform.position;
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
        _flower = f;
        flowerFather.GetComponent<FlowerBunch>().type = f;
        for (int i = 0; i < displayFlowers.Length; i++)
        {
            if (displayFlowers[i].gameObject.name != gameObject.name)
            {
                renderers[i].materials = f.flowerModel.flowerMaterial.sharedMaterials;
                displayFlowers[i].mesh = f.flowerModel.flowerMesh.sharedMesh;
            }
        }
    }

    public void RemoveAction()
    {
        print("out of Bucket");
    }

    public bool DropAction(GameObject a = null)
    {
        return flowerBunch == a.GetComponent<FlowerBunch>();
    }
}
