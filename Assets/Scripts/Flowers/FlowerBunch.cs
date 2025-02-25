using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBunch : MonoBehaviour, IDragable, IResteable
{
    private BucketOfFlowers father;
    public bool used = false;
    public Flower type;

    bool _canBeDragged;

    public AudioSource EffecSound;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    public bool canBeDragged { get => _canBeDragged; set => _canBeDragged = value; }

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        canBeDragged = true;
        father = GetComponentInParent<BucketOfFlowers>();
    }

    public GameObject ObjectsToBeDraged(ref Vector3 positions)
    {
        GameManager.instance.AM.PlayEffect(EffecSound);
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
        _boxCollider.enabled = true;
        _meshRenderer.enabled = true;
        transform.SetParent(father.transform);
    }
}
