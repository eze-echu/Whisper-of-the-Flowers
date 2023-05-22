using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBunch : MonoBehaviour, IDragable, IResteable
{
    private BucketOfFlowers father;
    public bool used = false;
    public Flower type;

    bool _canBeDragged;
    public bool canBeDragged { get => _canBeDragged; set => _canBeDragged = value; }

    private void Start()
    {
        canBeDragged = true;
        father = GetComponentInParent<BucketOfFlowers>();
    }

    public GameObject ObjectsToBeDraged(ref Vector3 positions)
    {
        positions = transform.position;
        return gameObject;
    }

    public bool WasUsed()
    {
        return used;
    }

    public void ResetToOriginalState()
    {
        used = true;
        transform.position = father.OGflowerPosition;
        GetComponent<BoxCollider>().enabled = true;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        transform.SetParent(father.transform);
    }
}
