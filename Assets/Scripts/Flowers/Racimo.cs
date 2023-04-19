using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Racimo : MonoBehaviour
{
    [SerializeField] List<GameObject> _posOfFlowers;
    //[SerializeField] GameObject _principal;
    //[SerializeField] GameObject _secondary;
    //[SerializeField] GameObject _terceary;

    private Vector3 _lastposition;

    FlowerValues values;


    public void Start()
    {
        _lastposition = transform.position;

        values.intent = 0;
        values.formality = 0;
        values.intentMultiplier = 0;
        values.intentMultiplier = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //codigo anterior anterior de devolver objeto
        /*
        if (collision.collider.CompareTag("dontzone"))
        {

            transform.position = _lastposition;
        }
        */

        //buscar manera de reducir cantidad de ifs
        if (collision.transform.parent == null && collision.gameObject.GetComponent<IDragable>() != null)
        {
            //Esto creo que se podria separar en dos funciones "ToStickAnObject" y "GetVariableFlowers"
            //para que quede mejor visualmente(creo que no respeta SOLID aun asi)
            foreach (var item in _posOfFlowers)
            {
                //print(item);
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

                    FlowerBunch childVariables = collision.transform.GetComponent<FlowerBunch>();
                    if (childVariables != null)
                    {
                        childVariables.used = true;
                        if (item == _posOfFlowers.FirstOrDefault())
                        {
                            values.message = childVariables.type.flowerValues.message;
                            //print(subject);
                        }

                        
                        if (item == _posOfFlowers.LastOrDefault())
                        {
                            values.intentMultiplier = childVariables.type.flowerValues.intentMultiplier;
                            values.formalityMultiplier = childVariables.type.flowerValues.formalityMultiplier;

                            print(values);
                            print(values);

                            Formula();
                           
                        }
                        else
                        {
                            values.intent += childVariables.type.flowerValues.intent;
                            values.formality += childVariables.type.flowerValues.formality;

                            print(values);
                            print(values);
                        }
                    }
                    break;
                }

         

            }
        }
    }

    //codigo anterior para devolver objetos
    /*
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("dontzone"))
        {
            print("se fue");
            transform.position = _lastposition;
        }
    }
    */

    private void Formula()
    {
        values.intent *= values.intentMultiplier;
        values.formality *= values.formalityMultiplier;

        print(values.intent);
        print(values.formality);
    }
}