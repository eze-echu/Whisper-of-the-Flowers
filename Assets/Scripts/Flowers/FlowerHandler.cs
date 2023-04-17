using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessageType
{
    Decrease_of_Love,
    Love,
    Faith,
    Foolishness
}
public class FlowerHandler : MonoBehaviour
{
    public Flower[] flowers;
    public BucketOfFlowers buckets;
    public GameObject scrollViewPort;

    private void Start()
    {
        foreach(var flower in flowers)
        {
            if (flower.available)
            {
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                GameObject b = Instantiate(buckets.gameObject);
                b.GetComponent<BucketOfFlowers>().flower = flower;
            }
        }
    }
}
