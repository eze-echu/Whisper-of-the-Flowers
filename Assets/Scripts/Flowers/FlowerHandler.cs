using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum FlowerMessageType
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

    public Transform trash;

    private void Start()
    {
        int i = 0; //Acts as X axis on the flower grid
        int j = 0; //Acts as Y Acis on the flower grid
        foreach(var flower in flowers)
        {
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
        RefreshFlowers();
        GameManager.Subscribe("DisableAllFlowers", DisableAllFlowers);
        GameManager.Subscribe("ResetWorkspace", ResetWorkspace);
        GameManager.Subscribe("EnableAllFlowers", EnableAllFlowers);
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("DisableAllFlowers", DisableAllFlowers);
        GameManager.Unsuscribe("ResetWorkspace", ResetWorkspace);
        GameManager.Unsuscribe("EnableAllFlowers", EnableAllFlowers);
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
            item.flowerFather.gameObject.GetComponent<BoxCollider>().enabled = false;
            item.flowerFather.gameObject.tag = "Untagged";
        }
    }
    public void EnableAllFlowers()
    {
        foreach (var item in bucketsOfFlowers)
        {
            //item.flowerFather.gameObject.SetActive(true);
            item.flowerFather.gameObject.GetComponent<BoxCollider>().enabled = true;
            item.flowerFather.gameObject.tag = "drag";
        }
    }
    private void RefreshFlowers(){
        int i = 0; //Acts as X axis on the flower grid
        int j = 0; //Acts as Y Acis on the flower grid
        foreach(var bucket in bucketsOfFlowers)
        {
            print("a");
            if (bucket.flower.available)
            {
                print("b");
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                if (i > 0 && i % GridWidth == 0)
                {
                    j++;
                    i = 0;
                }
                //GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(i, 0, j)), Quaternion.identity);
                bucket.transform.position = grid.CellToWorld(new Vector3Int(i, 0, j));
                i++;
                bucket.transform.SetParent(grid.transform);
                print("b");
            }
            else
            {
                bucket.transform.position = trash.position;
            }
        }
    }
    public void EnableNewFlower(Flower flowerToEnable){
        if(flowers.Contains(flowerToEnable)){
            flowers.Where(x=> x == flowerToEnable).First().available = true;
        }
    }
}
