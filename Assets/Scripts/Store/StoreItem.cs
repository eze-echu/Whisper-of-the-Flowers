using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StoreItem
{
    public string itemName;
    public int price;
    public bool isVisible;
    public GameObject itemPrefab; // Prefab del objeto en la tienda
}
