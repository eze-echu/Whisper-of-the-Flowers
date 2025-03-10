using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemUI : MonoBehaviour
{
   public List<StoreItem> flowers = new List<StoreItem>();
    public List<StoreItem> vases = new List<StoreItem>();
    public List<StoreItem> others = new List<StoreItem>(); 

    public Transform flowersPanel; 
    public Transform vasesPanel;   
    public Transform othersPanel;  

    public GameObject storeItemPrefab; // Prefab del botón/item de la tienda
    private int itemIndex = 1;


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
                string itemName = $"ItemStore{itemIndex++}"; // Genera un nombre único
                bool alreadyBought = LoadPurchaseStatus(itemName);
                
                Debug.Log($"Agregando item a la tienda: {item.itemName}");

                GameObject newItem = Instantiate(storeItemPrefab, targetPanel);
                //Store itemUI = newItem.GetComponent<Store>();
                Button itemButton = newItem.GetComponent<Button>();

                if (itemButton  == null)
                {
                    Debug.LogError("El prefab instanciado no tiene un componente Button.");
                    continue;
                }

                StoreItemClickHandler clickHandler = newItem.AddComponent<StoreItemClickHandler>();
                clickHandler.Setup(itemButton, itemName, alreadyBought);
                //itemUI.Setup(item);
            }
        }
    }

    bool LoadPurchaseStatus(string itemName)
    {
        var savedData = Save.LoadDirectly();
        return savedData.ContainsKey(itemName) && (bool)savedData[itemName];
    }
}
