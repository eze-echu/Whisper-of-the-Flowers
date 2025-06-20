using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flowers;
using JetBrains.Annotations;
using Racimo.Decoration;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;
using Racimo.Vase;

namespace Racimo
{
    public partial class Bouquet : MonoBehaviour, IDropZone, IOccupied, IResteable
    {
        [Serializable]
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

        [SerializeField] public Vase.Vase vase;
        [SerializeField] public Decoration.Decoration decoration;

        [SerializeField] private GameObject decorationObject;


        [SerializeField] List<Flowers> flowers;

        private int _occupied;

        private FlowerValues _values;
        private FlowerMessageType[] _messages;
        bool _ready;
        bool _canBeDragged;
        public static Bouquet Instance;

        [FormerlySerializedAs("effecSound")] [FormerlySerializedAs("EffecSound")]
        public AudioSource effectSound;

        private Workstations _currentWorkstations;


        [FormerlySerializedAs("_flowerPositions")] [SerializeField]
        private Transform[] flowerPositions;

        private BoxCollider _boxCollider;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void Start()
        {
            var a = VaseHandler.Instance.GetAvailableVaseObjects();
            vase = new Vase.Vase(
                vaseFilter: new List<MeshFilter>(GetComponents<MeshFilter>()),
                vaseModel: new List<MeshRenderer>(GetComponents<MeshRenderer>()),
                vase.GetVase() is null ? a[0] : vase.GetVase()
            );
            var b = DecorationHandler.Instance.GetAvailableDecorationObjects();
            decoration = new Decoration.Decoration(
                decorationFilter: new List<MeshFilter>(decorationObject.GetComponents<MeshFilter>()),
                decorationModel: new List<MeshRenderer>(decorationObject.GetComponents<MeshRenderer>()),
                decoration.GetDecoration() is null ? b[0] : decoration.GetDecoration()
            );
            decorationObject.SetActive(false);

            _boxCollider = GetComponent<BoxCollider>();
            SwitchWorkstation(Workstations.VaseStation);

            _values.intent = 0;
            _values.formality = 0;
            _values.intentMultiplier = 0;
            _values.intentMultiplier = 0;
            _values.message = FlowerMessageType.Null;
            _messages = new FlowerMessageType[3];


            _ready = false;
        }

        private void SwitchWorkstation(Workstations workstation)
        {
            _currentWorkstations = workstation;
            CameraController.instance.SwitchToSpecificCamera(_currentWorkstations);
            CameraController.instance.EnableCurrentCamera();
            MoveBouquet((uint)workstation);
        }

        private void MoveBouquet(uint table)
        {
            transform.position = flowerPositions[table].position;
            transform.rotation = flowerPositions[table].rotation;
        }


        public FlowerMessageType[] GetMessages()
        {
            return _messages;
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

        private void PlaySound()
        {
            GameManager.instance.AM.PlayEffect(effectSound);
        }

        public void ProceedToNextStation()
        {
            CameraController.instance.DisableUIs();
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
                        decorationObject.SetActive(true);
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

        public Workstations Workstation()
        {
            return _currentWorkstations;
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