using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBunch : MonoBehaviour, IDragable, IFusionable, IResteable
{
    private BucketOfFlowers father;
    public bool used = false;
    public Flower type;
    private void Start()
    {
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
        transform.SetParent(father.transform);
    }
}