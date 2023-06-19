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
    Foolishness,
    Justice,
    Hatred,
    Jealousy,
    Mourning
}
public class FlowerHandler : MonoBehaviour
{
    public Flower[] flowers;
    public BucketOfFlowers bucketPrefab;
    public Grid grid;
    [Range(1, 5)]
    public int GridWidth;
    private List<BucketOfFlowers> bucketsOfFlowers = new List<BucketOfFlowers>();
    public GameObject scrollViewPort;

    private void Start()
    {
        int i = 0; //Acts as X axis on the flower grid
        int j = 0; //Acts as Y Acis on the flower grid
        foreach(var flower in flowers)
        {
            print("a");
            if (flower.available)
            {
                print("a");
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                if (i > 0 && i % GridWidth == 0)
                {
                    j++;
                    i = 0;
                }
                GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(i, 0, j)), Quaternion.identity);
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

    public void DisableAllFlowers()
    {
        foreach (var item in bucketsOfFlowers)
        {
            print("Disable FlowerCollider");
            item.flowerFather.gameObject.SetActive(false);
        }
    }
    public void EnableAllFlowers()
    {
        foreach (var item in bucketsOfFlowers)
        {
            item.flowerFather.gameObject.SetActive(true);
        }
    }
}
