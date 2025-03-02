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
                var a = flowers[occupied];
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
                        MonoBehaviour.print("Not within the scope of the Switch");
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
                    MonoBehaviour.print("Not Ready");
                }

                gameObject.tag = "DropZone";
                _ready = false;
                occupied = 0;
            }
        }
    }
}