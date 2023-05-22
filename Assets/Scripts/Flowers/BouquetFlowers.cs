using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BouquetFlowers : MonoBehaviour
{
    private Flower _flower;
    public Flower flower { get => _flower; set => SetFlower(value); }
    Vector3 OGPosition;
    private List<MeshFilter> models;
    private List<MeshRenderer> materials;


    private void Start()
    {
        OGPosition = transform.position;
        models = new List<MeshFilter>(GetComponentsInChildren<MeshFilter>());
        materials = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
    }
    public bool canBeDragged { get => canBeDragged; set => canBeDragged = value; }

    public void SetFlower(Flower f)
    {
        print("Setting Flower");
        if (f)
        {
            foreach (var material in materials)
            {
                material.enabled = true;
                //material.sharedMesh = f.flowerModel.flowerMesh.sharedMesh;
                material.sharedMaterials = f.flowerModel.flowerMaterial.sharedMaterials;
            }
            foreach (var model in models)
            {
                model.sharedMesh = f.flowerModel.flowerMesh.sharedMesh;
            }
            /*materials.Select(x => { x.enabled = true; return x; });
            models.Select(x => { x.sharedMesh = f.flowerModel.flowerMesh.sharedMesh; return x; });
            materials.Select(x => { x.sharedMaterials = f.flowerModel.flowerMaterial.sharedMaterials; return x; });*/
        }
        else
        {
            foreach (MeshRenderer a in materials)
            {
                a.enabled = false;
            }
            materials.Select(x => { x.enabled = false; return x; });
            //Debug.LogError("Missing Flower for bouquet Flower");
        }
        _flower = f;
    }

    private Flower GetFlower()
    {
        return flower;
    }
}
