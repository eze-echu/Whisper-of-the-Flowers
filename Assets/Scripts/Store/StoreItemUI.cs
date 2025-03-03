using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemUI : MonoBehaviour
{
    public List<StoreItem> flowers = new List<StoreItem>();
    public List<StoreItem> vases = new List<StoreItem>();
    public List<StoreItem> others = new List<StoreItem>(); // Nombre temporal

    public Transform contentPanel; // El panel del ScrollView
    public GameObject storeItemPrefab; // Prefab del botón/item de la tienda

    void Start()
    {
        PopulateStore(flowers);
        PopulateStore(vases);
        PopulateStore(others);
    }

    void PopulateStore(List<StoreItem> items)
    {
        foreach (var item in items)
        {
            if (item.isVisible)
            {
                Debug.Log($"Agregando item a la tienda: {item.itemName}");

                GameObject newItem = Instantiate(storeItemPrefab, contentPanel);
                Store itemUI = newItem.GetComponent<Store>();

                if (itemUI == null)
                {
                    Debug.LogError("El prefab instanciado no tiene StoreItemUI. Revisa que el prefab esté bien configurado.");
                    continue;
                }

                itemUI.Setup(item);
            }
        }
    }
}
