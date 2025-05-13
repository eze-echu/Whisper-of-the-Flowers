using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// carga los ítems en la tienda y ubicarlos en sus respectivas secciones
/// </summary>
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
                Button itemButton = newItem.GetComponent<Button>();

                Image itemImageUI = newItem.GetComponentInChildren<Image>();
                if (itemImageUI != null && item.itemImage != null)
                {
                    itemImageUI.sprite = item.itemImage;
                }
                
                  TextMeshProUGUI priceText = newItem.GetComponentInChildren<TextMeshProUGUI>();
                if (priceText != null)
                {
                    priceText.text = $"${item.price}";
                }
                else
                {
                    Debug.LogWarning("No se encontró TextMeshProUGUI en el prefab.");
                }

                if (itemButton == null)
                {
                    Debug.LogError("El prefab instanciado no tiene un componente Button.");
                    continue;
                }

                // Asigna el StoreItem completo al StoreItemClickHandler
                StoreItemClickHandler clickHandler = newItem.AddComponent<StoreItemClickHandler>();
                clickHandler.Setup(itemButton, item, alreadyBought);  // Pasamos el StoreItem completo

                // Aquí también podrías añadir cualquier otra configuración si lo necesitas
            }
        }
    }

    bool LoadPurchaseStatus(string itemName)
    {
        //var savedData = Save.LoadDirectly();
        //return savedData.ContainsKey(itemName) && (bool)savedData[itemName];
        return false; // Implementa la carga del estado de la compra si es necesario
    }
}
