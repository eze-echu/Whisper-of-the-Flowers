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

    string _subject;
    int _intentValues;
    int _formalityValues;
    int _multiplierIntent;
    int _multiplierFormality;


    public void Start()
    {
        _lastposition = transform.position;

        _subject = null;
        _intentValues = 0;
        _formalityValues = 0;
        _multiplierIntent = 0;
        _multiplierFormality = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {

        /*
        if (collision.collider.CompareTag("dontzone"))
        {

            transform.position = _lastposition;
        }
        */

        //buscar manera de reducir cantidad de ifs
        if (collision.transform.parent == null)
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

                    IGetVariables childVariables = collision.transform.GetComponent<IGetVariables>();
                    if (childVariables != null)
                    {

                        if (item == _posOfFlowers.FirstOrDefault())
                        {
                            _subject = childVariables.Subject;
                            //print(subject);
                        }

                        
                        if (item == _posOfFlowers.LastOrDefault())
                        {
                            _multiplierIntent = childVariables.MultiplierIntent;
                            _multiplierFormality = childVariables.MultiplierFormality;

                            print(_multiplierIntent);
                            print(_multiplierFormality);

                            Formula();
                           
                        }
                        else
                        {
                            _intentValues += childVariables.Intent;
                            _formalityValues += childVariables.Formality;

                            print(_intentValues);
                            print(_formalityValues);
                        }
                    }
                    break;
                }

         

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("dontzone"))
        {
            print("se fue");
            transform.position = _lastposition;
        }
    }

    private void Formula()
    {
        _intentValues = _intentValues * _multiplierIntent;
        _formalityValues = _formalityValues * _multiplierFormality;

        print(_intentValues);
        print(_formalityValues);
    }

    public T GetVariable<T>(int ID)
    {
        switch(ID)
        {
            case 1:
                return (T)(object)_subject;
                

            case 2:
                return (T)(object)_intentValues;
                

            case 3:
                return (T)(object)_formalityValues;
                
                
            case 4:
                return (T)(object)_multiplierIntent;
                

            case 5:
                return (T)(object)_multiplierFormality;
                

            default:
                print("no");
                return default;
                
        }

    }

}