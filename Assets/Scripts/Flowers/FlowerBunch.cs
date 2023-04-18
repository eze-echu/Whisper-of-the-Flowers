using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBunch : MonoBehaviour, IDragable
{
    private BucketOfFlowers father;
    private void Start()
    {
        father = GetComponentInParent<BucketOfFlowers>();
    }
    public void Move(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public GameObject ObjectsToBeDraged(ref Vector3 positions)
    {
        positions = transform.position;
        return gameObject;
    }
}
