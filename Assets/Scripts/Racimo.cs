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

    string subject;
    List<int> _intentValues;
    List<int> _formalityValues;
    int multiplierIntent;
    int multiplierFormality;


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
                print(item);
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
                        //int intent = childVariables.Intent;
                        //int formality = childVariables.Formality;
                        //int multiplierIntent = childVariables.MultiplierIntent;
                        //int multiplierFormality = childVariables.MultiplierFormality;

                        if (item == _posOfFlowers.FirstOrDefault())
                        {
                            subject = childVariables.Subject;
                            //print(subject);
                        }


                        
                        if (item == _posOfFlowers.LastOrDefault())
                        {
                            multiplierIntent = childVariables.MultiplierIntent;
                            multiplierFormality = childVariables.MultiplierFormality;

                            print(multiplierIntent);
                            print(multiplierFormality);
                            /*
                            int intent =+ childVariables.Intent;
                            int formality =+ childVariables.Formality;
                            //_intentValues.Add(intent);
                            //_formalityValues.Add(childVariables.Formality);
                            */
                        }
                        else
                        {
                            int intent = childVariables.Intent;
                            int formality = childVariables.Formality;

                            print(intent);
                            print(formality);
                        }
                    }
                    break;
                }

         

            }
        }
    }

    private void Formula()
    {

    }

}