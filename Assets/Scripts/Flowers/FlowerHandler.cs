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
    public static FlowerHandler instance;
    private int x, y;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        RevertFlowersToUnavailable();
        //RefreshFlowers();
        GameManager.Subscribe("DisableAllFlowers", DisableAllFlowers);
        GameManager.Subscribe("ResetWorkspace", ResetWorkspace);
        GameManager.Subscribe("EnableAllFlowers", EnableAllFlowers);
        GameManager.Subscribe("RefreshFlowers", RefreshFlowers);
        GameManager.Subscribe("CleanFlowers", CleanFlowers);
        GameManager.Subscribe("RevertFlowersToUnavailable", RevertFlowersToUnavailable);
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("DisableAllFlowers", DisableAllFlowers);
        GameManager.Unsuscribe("ResetWorkspace", ResetWorkspace);
        GameManager.Unsuscribe("EnableAllFlowers", EnableAllFlowers);
        GameManager.Unsuscribe("RefreshFlowers", RefreshFlowers);
        GameManager.Unsuscribe("CleanFlowers", CleanFlowers);
        GameManager.Unsuscribe("RevertFlowersToUnavailable", RevertFlowersToUnavailable);
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
        x = 0; //Acts as X axis on the flower grid
        y = 0; //Acts as Y Acis on the flower grid
        //Limpieza previo al crear el set
        foreach(var flower in flowers)
        {
            print("a");
            if (flower.available)
            {
                print("a");
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
                b.transform.SetParent(scrollViewPort.transform, true);*/
                if (x > 0 && x % GridWidth == 0)
                {
                    y++;
                    x = 0;
                }
                GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(x, 0, y)), Quaternion.identity);
                x++;
                b.transform.SetParent(grid.transform);
                var c = b.GetComponent<BucketOfFlowers>();
                c.flower = flower;
                bucketsOfFlowers.Add(c);
                print("b");
            }
        }
    }
    public void EnableNewFlower(Flower flowerToEnable){
        if(flowers.Contains(flowerToEnable)){
            flowers.Where(x=> x == flowerToEnable).First().available = true;
            if (x > 0 && x % GridWidth == 0)
            {
                y++;
                x = 0;
            }
            GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(x, 0, y)), Quaternion.identity);
            x++;
            b.transform.SetParent(grid.transform);
            var c = b.GetComponent<BucketOfFlowers>();
            c.flower = flowerToEnable;
            bucketsOfFlowers.Add(c);
            print("b");
        }
    }
    private void CleanFlowers(){
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in grid.transform)
        {
            children.Add(child.gameObject);
        }
        foreach (BucketOfFlowers child in bucketsOfFlowers)
        {
            Destroy(child.gameObject);
        }
    }

    private void RevertFlowersToUnavailable(){
        foreach(var flower in flowers)
        {
            flower.available = false;
        }
    }
}
