using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class Bouquet : MonoBehaviour, IDragable, IDropZone, IOccupied, IResteable
{
    //[SerializeField] List<GameObject> _posOfFlowers;
    //[SerializeField] GameObject _principal;
    //[SerializeField] GameObject _secondary;
    //[SerializeField] GameObject _terceary;

    [SerializeField]
    List<BouquetFlowers> flowers;

    private int occupied = 0;

    private Vector3 _lastposition;
    private quaternion _lastRotation;

    FlowerValues values;
    private BoxCollider boxCollider;
    bool _ready;
    public bool canBeDragged { get => _canBeDragged; set => _canBeDragged = value; }
    bool _canBeDragged;

    public void Start()
    {
        _lastposition = transform.position;
        _lastRotation = transform.rotation;
        boxCollider = GetComponent<BoxCollider>();

        values.intent = 0;
        values.formality = 0;
        values.intentMultiplier = 0;
        values.intentMultiplier = 0;
        values.message = FlowerMessageType.Null;
        canBeDragged = false;

        _ready = false;
    }

    public void FlowerAdded(FlowerBunch flowerBunch)
    {
        print("Added Flower");
        bool areFilled = true;
        if (flowerBunch) {
            BouquetFlowers a = flowers[occupied];
            if (!a.flower)
            {
                areFilled = false;
            }
            flowerBunch.ResetToOriginalState();
            flowers[occupied].flower = flowerBunch.type;
            /*flowerBunch.transform.SetParent(flowers[occupied].transform);
            flowerBunch.GetComponentInChildren<MeshRenderer>().enabled = false;
            flowerBunch.GetComponent<BoxCollider>().enabled = false;*/
            switch (occupied)
            {
                case 0:
                    values.message = a?.flower.flowerValues.message ?? FlowerMessageType.Null;
                    occupied++;
                    //print($"{a.type.flowerValues.message}");
                    break;
                case 1:
                    values.intent = a?.flower.flowerValues.intent ?? 0;
                    values.formality = a?.flower.flowerValues.formality ?? 0;
                    occupied++;
                    break;
                case 2:
                    values.intentMultiplier = a?.flower.flowerValues.intentMultiplier ?? 0;
                    values.formalityMultiplier = a?.flower.flowerValues.formalityMultiplier ?? 0;
                    occupied++;
                    break;
                default:
                    print("Not within the scope of the Switch");
                    break;
            }
            if(occupied == flowers.Count)
            {
                areFilled = true;
            }
            if (values.message != FlowerMessageType.Null && areFilled)
            {
                _ready = true;
                /*foreach (var item in flowers)
                {
                    item.GetComponent<BoxCollider>().enabled = false;
                    print(item.transform.childCount);
                    *//*if (item.transform.childCount == 1)
                    {
                        item.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                    }*//*
                    print("Ready");
                }*/
                canBeDragged = true;
                gameObject.tag = "Occupied";
                boxCollider.enabled = true;
            }
            else
            {
                gameObject.tag = "DropZone";
            }
        }
        else
        {
            foreach (var item in flowers)
            {
                item.SetFlower(null);
                print("Not Ready");
            }
            gameObject.tag = "DropZone";
            _ready = false;
            canBeDragged = false;
            occupied = 0;
        }
    }

    public void SendVariableToStoryManager()
    {
        StoryController storyController = StoryController.instance;

        
        if (_ready)//values.message != null && values.intent != 0 && values.formality != 0) // poner values.MultiplierFormality
        {
            canBeDragged = false;
            //string subject = values.message.ToString();
            storyController.ProgressStory(values.message, values.intent, values.formality);
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
        CameraController.instance.SwitchToSpecificCamera(1);
        transform.Rotate(new Vector3(0, -45, 0));
        return gameObject;
    }

    public bool WasUsed()
    {
        throw new System.NotImplementedException();
    }

    public bool DropAction(GameObject a = null)
    {
        if (!_ready && a.GetComponent<FlowerBunch>())
        {
            FlowerAdded(a.GetComponent<FlowerBunch>());
        }
        else if (_ready){
            gameObject.tag = "Occupied";
        }
        return true;
    }

    public void RemoveAction()
    {
        //FlowerAdded();
    }

    public void ResetToOriginalState()
    {
        canBeDragged = false;
        transform.position = _lastposition;
        transform.SetParent(null);
        transform.rotation = _lastRotation;
        GetComponent<BoxCollider>().enabled = true;
        FlowerAdded(null);
    }

    public FlowerValues GetValues()
    {
        return values;
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
