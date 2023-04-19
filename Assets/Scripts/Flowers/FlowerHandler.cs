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
    public BucketOfFlowers bucketPrefab;
    private List<BucketOfFlowers> bucketsOfFlowers = new List<BucketOfFlowers>();
    public GameObject scrollViewPort;

    private void Start()
    {
        foreach(var flower in flowers)
        {
            print("a");
            if (flower.available)
            {
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                GameObject b = Instantiate(bucketPrefab.gameObject);
                var c = b.GetComponent<BucketOfFlowers>();
                c.flower = flower;
                bucketsOfFlowers.Add(c);
                print("b");
            }
        }
    }

    public void ResetWorkspace()
    {

    }
}
