using System.Collections;
using System.Collections.Generic;
using Flowers;
using UnityEngine;

namespace Racimo
{
    public partial class Bouquet : MonoBehaviour, IDropZone, IOccupied, IResteable
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
        [SerializeField] List<global::Racimo.Flowers> flowers;

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



        private IEnumerator WaitFewSeconds(float time)
        {
            handInBefore();
            yield return new WaitForSeconds(time);
            handInAfter();
        }
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