using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBunch : MonoBehaviour, IDragable, IFusionable
{
    private BucketOfFlowers father;
    private void Start()
    {
        father = GetComponentInParent<BucketOfFlowers>();
    }

    public GameObject ObjectsToBeDraged(ref Vector3 positions)
    {
        positions = transform.position;
        return gameObject;
    }
}
