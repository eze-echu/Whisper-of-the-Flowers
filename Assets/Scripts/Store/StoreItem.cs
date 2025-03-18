using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// estructura de un ítem en la tienda.
/// </summary>
[System.Serializable]
public class StoreItem
{
    public string itemName;
    public int price;
    public bool isVisible;
    public GameObject itemPrefab; // Prefab del objeto en la tienda
    public StoreEffect effect; // Referencia al efecto que tiene este ítem
}
