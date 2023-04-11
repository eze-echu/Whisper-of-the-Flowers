using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racimo : MonoBehaviour
{
    [SerializeField] List<GameObject> _posOfFlowers;
    //[SerializeField] GameObject _principal;
    //[SerializeField] GameObject _secondary;
    //[SerializeField] GameObject _terceary;

    private Vector3 _lastposition;


    public void Start()
    {
        _lastposition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.collider.CompareTag("dontzone"))
        {

            transform.position = _lastposition;
        }

        if (collision.transform.parent == null)
        {
            // collision.transform.SetParent(_principal.transform);
            foreach (var item in _posOfFlowers)
            {
                if (item.transform.childCount == 0)
                {
                    collision.transform.SetParent(item.transform);

                    Transform transformCollider = collision.transform.GetComponent<Transform>();
                    transformCollider.position = item.transform.position;

                    Collider childCollider = collision.transform.GetComponent<Collider>();
                    if (childCollider != null)
                    {
                        childCollider.enabled = false;
                    }
                }

            }


        }


    }


}