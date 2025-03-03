using Flowers;
using UnityEngine;

namespace Racimo
{
    public partial class Bouquet
    {
        public void FlowerAdded(FlowerBunch flowerBunch)
        {
            print("Added Flower");
            var areFilled = true;
            if (flowerBunch)
            {
                var a = flowers[_occupied];
                if (!a.flower)
                {
                    areFilled = false;
                }

                flowerBunch.ResetToOriginalState();
                flowers[_occupied].flower = flowerBunch.type;
                /*flowerBunch.transform.SetParent(flowers[occupied].transform);
            flowerBunch.GetComponentInChildren<MeshRenderer>().enabled = false;
            flowerBunch.GetComponent<BoxCollider>().enabled = false;*/
                if (a.flower is not { } b) return;
                switch (_occupied)
                {
                    case 0:
                        _values.message = b.flowerValues.message;
                        _messages[0] = _values.message;
                        _occupied++;
                        PlaySound();
                        //print($"{a.type.flowerValues.message}");
                        break;
                    case 1:
                        _messages[1] = b.flowerValues.message;
                        _values.intent = b.flowerValues.intent;
                        _values.formality = b.flowerValues.formality;
                        _occupied++;
                        PlaySound();
                        break;
                    case 2:
                        _messages[2] = b.flowerValues.message;
                        _values.intentMultiplier = b.flowerValues.intentMultiplier;
                        _values.formalityMultiplier = b.flowerValues.formalityMultiplier;
                        _occupied++;
                        PlaySound();
                        break;
                    default:
                        MonoBehaviour.print("Not within the scope of the Switch");
                        break;
                }

                if (_occupied == flowers.Count)
                {
                    areFilled = true;
                }

                if (_values.message != FlowerMessageType.Null && areFilled)
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
                    MonoBehaviour.print("Not Ready");
                }

                gameObject.tag = "DropZone";
                _ready = false;
                _occupied = 0;
            }
        }
    }
}