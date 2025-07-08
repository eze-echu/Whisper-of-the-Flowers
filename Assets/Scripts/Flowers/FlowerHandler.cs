using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Flowers
{
    public enum FlowerMessageType
    {
        Null,
        DecreaseOfLove,
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
        [Range(1, 5)] public int GridWidth;
        private List<BucketOfFlowers> bucketsOfFlowers = new List<BucketOfFlowers>();
        //public GameObject scrollViewPort;

        public Transform trash;

        public static FlowerHandler instance;

        public void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            int i = 0; //Acts as X axis on the flower grid
            int j = 0; //Acts as Y Acis on the flower grid
            foreach (var flower in flowers)
            {
                /*var b = Instantiate(buckets.gameObject, scrollViewPort.transform);
            b.transform.SetParent(scrollViewPort.transform, true);*/
                if (i > 0 && i % GridWidth == 0)
                {
                    j++;
                    i = 0;
                }

                GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(i, 0, j)),
                    Quaternion.identity);
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

        private void OnDestroy()
        {
            GameManager.Unsuscribe("DisableAllFlowers", DisableAllFlowers);
            GameManager.Unsuscribe("ResetWorkspace", ResetWorkspace);
            GameManager.Unsuscribe("EnableAllFlowers", EnableAllFlowers);
        }

        public void ResetWorkspace()
        {
            foreach (var a in bucketsOfFlowers)
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

        private void RefreshFlowers()
        {
            int i = 0; //Acts as X axis on the flower grid
            int j = 0; //Acts as Y Acis on the flower grid
            foreach (var bucket in bucketsOfFlowers)
            {
                print("a");
                if (bucket.flower.available)
                {
                    print("b");
                    //bucket.transform.SetParent(scrollViewPort.transform, true);
                    if (i > 0 && i % GridWidth == 0)
                    {
                        j++;
                        i = 0;
                    }

                    //GameObject b = Instantiate(bucketPrefab.gameObject, grid.CellToWorld(new Vector3Int(i, 0, j)), Quaternion.identity);
                    Vector3 WorldPos = grid.CellToWorld(new Vector3Int(i, 0, j));
                    bucket.OGflowerPosition = new Vector3(WorldPos.x, bucket.OGflowerPosition.y, WorldPos.z);
                    bucket.transform.position = WorldPos;
                    i++;
                    bucket.transform.SetParent(grid.transform);
                    print("b");
                }
                else
                {
                    bucket.OGflowerPosition = new Vector3(trash.transform.position.x, bucket.OGflowerPosition.y,
                        trash.transform.position.z);
                    bucket.transform.position = trash.transform.position;
                    bucket.transform.SetParent(trash.transform.transform);
                }
            }
        }

        public void EnableNewFlower(Flower flowerToEnable)
        {
            if (flowers.Contains(flowerToEnable))
            {
                flowers.Where(x => x == flowerToEnable).First().available = true;
            }
        }
        public FlowerMessageType[] GetFlowerMessages()
        {
            return (from f in flowers where f.available select f.flowerValues.message).ToArray();
            return System.Enum.GetValues(typeof(FlowerMessageType)).Cast<FlowerMessageType>().ToArray();
        }

        public Flower GetFlowerByMessage(FlowerMessageType message)
        {
            try
            {
                return flowers.Single(f => f.flowerValues.message == message && f.available);

            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    Console.WriteLine("No flower matches the message or multiple flowers match the message.");
                }
                else
                {
                    Console.WriteLine("An unexpected error occurred: " + e.Message);
                }
                throw;
            }
        }
    }
}