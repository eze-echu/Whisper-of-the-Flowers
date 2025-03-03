using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemUI : MonoBehaviour
{
   public List<StoreItem> flowers = new List<StoreItem>();
    public List<StoreItem> vases = new List<StoreItem>();
    public List<StoreItem> others = new List<StoreItem>(); // Nombre temporal

    public Transform flowersPanel; // Panel del ScrollView para flores
    public Transform vasesPanel;   // Panel del ScrollView para jarrones
    public Transform othersPanel;  // Panel del ScrollView para otros ítems

    public GameObject storeItemPrefab; // Prefab del botón/item de la tienda

    void Start()
    {
        PopulateStore(flowers, flowersPanel);
        PopulateStore(vases, vasesPanel);
        PopulateStore(others, othersPanel);
    }

    void PopulateStore(List<StoreItem> items, Transform targetPanel)
    {
        foreach (var item in items)
        {
            if (item.isVisible)
            {
                Debug.Log($"Agregando item a la tienda: {item.itemName}");

                GameObject newItem = Instantiate(storeItemPrefab, targetPanel);
                Store itemUI = newItem.GetComponent<Store>();

                if (itemUI == null)
                {
                    Debug.LogError("El prefab instanciado no tiene Store. Revisa que el prefab esté bien configurado.");
                    continue;
                }

                itemUI.Setup(item);
            }
        }
    }
}
