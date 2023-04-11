using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFather : MonoBehaviour, IDragable, IFusionable
{
    public string message;

    public int[,] values = new int[2, 2];


    private Vector3 _lastposition;

    virtual public void Start()
    {
        _lastposition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        // Si el objeto seleccionado toca un collider no deseado, se establece su posición en la última posición almacenada
        if (collision.collider.CompareTag("dontzone"))
        {
            
            transform.position = _lastposition;
        }
    }
}