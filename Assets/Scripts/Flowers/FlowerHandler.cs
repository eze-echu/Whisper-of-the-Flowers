using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessageType
{
    Null,
    Decrease_of_Love,
    Love,
    Faith,
    Foolishness
}
public class FlowerHandler : MonoBehaviour
{
    public Flower[] flowers;
    public BucketOfFlowers bucketPrefab;
    public Grid grid;
    private List<BucketOfFlowers> bucketsOfFlowers = new List<BucketOfFlowers>();
    public GameObject scrollViewPort;

    private void Start()
    {
        int i = 0;
        foreach(var flower in flowers)
        {
            print("a");
            if (flower.available)
            {
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(i, 0, 0)), Quaternion.identity);
                i++;
                b.transform.SetParent(grid.transform);
                var c = b.GetComponent<BucketOfFlowers>();
                c.flower = flower;
                bucketsOfFlowers.Add(c);
                print("b");
            }
        }
    }

    public void ResetWorkspace()
    {
        foreach(BucketOfFlowers a in bucketsOfFlowers)
        {
            a.ResetToOriginalState();
        }
    }
}
