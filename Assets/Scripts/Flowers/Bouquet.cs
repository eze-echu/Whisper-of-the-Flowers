using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Flowers;
using Systems;
using UnityEngine.UIElements;
using Unity.Mathematics;
using UnityEngine.Serialization;

public class Bouquet : MonoBehaviour, IDropZone, IOccupied, IResteable
{
    public enum Workstations
    {
        VaseStation,
        FlowerStation,
        DeliveryStation
    }

    //[SerializeField] List<GameObject> _posOfFlowers;
    //[SerializeField] GameObject _principal;
    //[SerializeField] GameObject _secondary;
    //[SerializeField] GameObject _terceary;
    private delegate void handInActions();

    private handInActions handInAfter;
    private handInActions handInBefore;

    [SerializeField] private PartycleController partycleController;
    public AudioSource ParticleEffectSound;
    [SerializeField] List<BouquetFlowers> flowers;

    private int occupied = 0;

    FlowerValues values;
    private FlowerMessageType[] messages;
    bool _ready;
    bool _canBeDragged;
    public static Bouquet instance;

    public AudioSource EffecSound;

    private Workstations _currentWorkstations;

    [SerializeField] private Transform[] _flowerPositions;
    private BoxCollider _boxCollider;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        SwitchWorkstation(Workstations.VaseStation);

        values.intent = 0;
        values.formality = 0;
        values.intentMultiplier = 0;
        values.intentMultiplier = 0;
        values.message = FlowerMessageType.Null;
        messages = new FlowerMessageType[3];


        _ready = false;
    }

    private void SwitchWorkstation(Workstations workstation)
    {
        _currentWorkstations = workstation;
        MoveBouquet((uint)workstation);
    }

    private void MoveBouquet(uint table)
    {
        transform.position = _flowerPositions[table].position;
        transform.rotation = _flowerPositions[table].rotation;
    }


    public void FlowerAdded(FlowerBunch flowerBunch)
    {
        print("Added Flower");
        bool areFilled = true;
        if (flowerBunch)
        {
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
            if (a.flower is not { } b) return;
            switch (occupied)
            {
                case 0:
                    values.message = b.flowerValues.message;
                    messages[0] = values.message;
                    occupied++;
                    PlaySound();
                    //print($"{a.type.flowerValues.message}");
                    break;
                case 1:
                    messages[1] = b.flowerValues.message;
                    values.intent = b.flowerValues.intent;
                    values.formality = b.flowerValues.formality;
                    occupied++;
                    PlaySound();
                    break;
                case 2:
                    messages[2] = b.flowerValues.message;
                    values.intentMultiplier = b.flowerValues.intentMultiplier;
                    values.formalityMultiplier = b.flowerValues.formalityMultiplier;
                    occupied++;
                    PlaySound();
                    break;
                default:
                    print("Not within the scope of the Switch");
                    break;
            }

            if (occupied == flowers.Count)
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
                    */ /*if (item.transform.childCount == 1)
                    {
                        item.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                    }*/ /*
                    print("Ready");
                }*/
                gameObject.tag = "Occupied";
                // boxCollider.enabled = true;
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
            occupied = 0;
        }
    }

    // public GameObject ObjectsToBeDraged(ref Vector3 positions)
    // {
    //     positions = transform.position;
    //     CameraController.instance.SwitchToSpecificCamera(1);
    //     transform.Rotate(new Vector3(0, -45, 0));
    //     return gameObject;
    // }

    public bool WasUsed()
    {
        throw new System.NotImplementedException();
    }

    public FlowerMessageType[] GetMessages()
    {
        return messages;
    }

    public bool DropAction(GameObject a = null)
    {
        if (!_ready && a && a.GetComponent<FlowerBunch>() is { } b)
        {
            FlowerAdded(b);
        }
        else if (_ready)
        {
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
        _currentWorkstations = Workstations.VaseStation;
        MoveBouquet((uint)_currentWorkstations);
        transform.SetParent(null);
        _boxCollider.enabled = true;
        FlowerAdded(null);
    }

    public FlowerValues GetValues()
    {
        return values;
    }

    private void PlaySound()
    {
        GameManager.instance.AM.PlayEffect(EffecSound);
    }

    public void ProceedToNextStation()
    {
        switch (_currentWorkstations)
        {
            case Workstations.DeliveryStation:
                HandIn();
                break;
            case Workstations.VaseStation:
                SwitchWorkstation(Workstations.FlowerStation);
                _currentWorkstations = Workstations.FlowerStation;
                break;
            case Workstations.FlowerStation:
                if (_ready)
                {
                    SwitchWorkstation(Workstations.DeliveryStation);
                    _currentWorkstations = Workstations.DeliveryStation;
                }
                break;
            default:
                SwitchWorkstation(0);
                break;
        }
    }

    private void HandIn()
    {
        print("HandIN");
        gameObject.tag = "DropZone";
        handInBefore = delegate
        {
            //GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues...");
            GameManager.instance.AM.PlayEffect(ParticleEffectSound);
            GameManager.Trigger("DisableWorkspace");
            var grade = GameState.Instance.OrderSystem.CompleteOrder(this);
            print("Grade: " + grade);
            if (grade == 0)
            {
                partycleController.PlayParticle(2);
            }
            else if (Mathf.Approximately(grade, 1f))
            {
                partycleController.PlayParticle(3);
            }
            else
            {
                partycleController.PlayParticle(0);
            }

            GameState.Instance.AddRequestReward(Mathf.Clamp(grade, 0.3f, 1f));
            // if (intent == 5 && message == "Love")
            // {
            //     partycleController.PlayParticle(3);
            // }
            // else if (intent == -5 && message == "Hatred")
            // {
            //     partycleController.PlayParticle(2);
            // }
            // else
            // {
            //     partycleController.PlayParticle(message == "Decrease_of_Love" || message == "Jealousy" || message == "Mourning" || message == "Hatred" ? 0 : 1);
            // }
            GameState.PauseGame();
            // StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));
        };
        handInAfter = delegate
        {
            partycleController.StopAllParticles();
            //StartCoroutine(GameManager.instance.Fc.FadeInAndOutCoroutine("Un Tiempo Despues..."));
            // a?.transform.GetComponent<Bouquet>()?.SendVariableToStoryManager();
            FlowerHandler.instance.ResetWorkspace();
            ResetToOriginalState();
            GameState.Instance.NewRequest();
            GameState.ResumeGame();
        };
        StartCoroutine(WaitFewSeconds(3));
    }

    private IEnumerator WaitFewSeconds(float time)
    {
        handInBefore();
        yield return new WaitForSeconds(time);
        handInAfter();
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
    */ /*
    if (collision.collider.CompareTag("dontzone"))
    {

        transform.position = _lastposition;
    }
    */ /*

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

                */ /*Collider childCollider = collision.transform.GetComponent<Collider>();
                if (childCollider != null)
                {
                    childCollider.enabled = false;
                }*/ /*

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