using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bouquet : MonoBehaviour, IDragable, IDropZone, IOccupied, IResteable
{
    [SerializeField] List<GameObject> _posOfFlowers;
    //[SerializeField] GameObject _principal;
    //[SerializeField] GameObject _secondary;
    //[SerializeField] GameObject _terceary;

    private Vector3 _lastposition;

    FlowerValues values;

    bool _ready;

    public bool canBeDragged { get => canBeDragged; set => canBeDragged = value; }

    public void Start()
    {
        _lastposition = transform.position;

        values.intent = 0;
        values.formality = 0;
        values.intentMultiplier = 0;
        values.intentMultiplier = 0;
        values.message = MessageType.Null;

        _ready = false;
    }

    public void FlowerAdded()
    {
        print("Added Flower");
        bool areFilled = true;
        for (int i = 0; i < _posOfFlowers.Count; i++)
        {
            FlowerBunch a = _posOfFlowers[i].GetComponentInChildren<FlowerBunch>();
            if (!a)
            {
                areFilled = false;
            }
            switch (i)
            {
                case 0:
                    values.message = a?.type.flowerValues.message ?? MessageType.Null;
                    //print($"{a.type.flowerValues.message}");
                    break;
                case 1:
                    values.intent = a?.type.flowerValues.intent ?? 0;
                    values.formality = a?.type.flowerValues.formality ?? 0;
                    break;
                case 2:
                    values.intentMultiplier = a?.type.flowerValues.intentMultiplier ?? 0;
                    values.formalityMultiplier = a?.type.flowerValues.formalityMultiplier ?? 0;
                    break;
                default:
                    print("Not within the scope of the Switch");
                    break;
            }
        }
        if (values.message != MessageType.Null && areFilled)
        {
            _ready = true;
            foreach (var item in _posOfFlowers)
            {
                item.GetComponent<BoxCollider>().enabled = false;
                print(item.transform.childCount);
                if(item.transform.childCount == 1)
                {
                    item.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                }
                print("Ready");
            }
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            foreach (var item in _posOfFlowers)
            {
                item.GetComponent<BoxCollider>().enabled = true;
                print("Not Ready");
            }
            _ready = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void SendVariableToStoryManager()
    {
        StoryController storyController = FindObjectOfType<StoryController>();

        
        if (_ready)//values.message != null && values.intent != 0 && values.formality != 0) // poner values.MultiplierFormality
        {
            
            string subject = values.message.ToString();
            storyController.HandleStory(subject, values.intent, values.formality);
        }
        

    }

    private void Formula()
    {
        values.intent *= values.intentMultiplier;
        values.formality *= values.formalityMultiplier;

        print(values.intent);
        print(values.formality);
    }

    public GameObject ObjectsToBeDraged(ref Vector3 positions)
    {
        positions = transform.position;
        return gameObject;
    }

    public bool WasUsed()
    {
        throw new System.NotImplementedException();
    }

    public bool DropAction(GameObject a = null)
    {
        if (!_ready)
        {
            FlowerAdded();
        }
        else
        {
            SendVariableToStoryManager();
        }
        return true;
    }

    public void RemoveAction()
    {
        FlowerAdded();
    }

    public void ResetToOriginalState()
    {
        transform.position = _lastposition;
        foreach (var item in _posOfFlowers)
        {
            item.tag = "DropZone";
        }
        FlowerAdded();
    }
}

/*private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("give"))
    {
        SendVariableToStoryManager();
    }
}*/

/*private void OnTriggerEnter(Collision collision)
{
    //codigo anterior anterior de devolver objeto
    *//*
    if (collision.collider.CompareTag("dontzone"))
    {

        transform.position = _lastposition;
    }
    *//*

    //buscar manera de reducir cantidad de ifs
    if (collision.gameObject.GetComponent<IDragable>() != null)
    {
        print("Detectado");
        //Esto creo que se podria separar en dos funciones "ToStickAnObject" y "GetVariableFlowers"
        //para que quede mejor visualmente(creo que no respeta SOLID aun asi)
        foreach (var item in _posOfFlowers)
        {

            if (item.transform.childCount == 0)
            {
                collision.transform.SetParent(item.transform);

                Transform transformCollider = collision.transform.GetComponent<Transform>();
                transformCollider.position = item.transform.position;

                *//*Collider childCollider = collision.transform.GetComponent<Collider>();
                if (childCollider != null)
                {
                    childCollider.enabled = false;
                }*//*

                FlowerBunch childVariables = collision.transform.GetComponent<FlowerBunch>();
                //print(childVariables);
                if (childVariables != null)
                {
                    print("es flowerbunch");
                    childVariables.used = true;
                    if (item == _posOfFlowers.FirstOrDefault())
                    {
                        values.message = childVariables.type.flowerValues.message;
                        print(values.message);
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

                        _ready = true;
                        SendVariableToStoryManager();
                        print(values);
                        print(values);
                    }
                }
                break;
            }



        }
    }
}*/

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
