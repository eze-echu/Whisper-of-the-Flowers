using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Systems;
/// <summary>
/// maneja la presentación de un ítem en la UI de la tienda. Recibe un StoreItem y actualiza los textos de nombre y precio, además de asignar el evento de compra al botón.
/// </summary>
public class Store : MonoBehaviour
{
    /*
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;

    private StoreItem itemData;

    public void Setup(StoreItem item)
    {
        Debug.Log("hace algo");
        itemData = item;
        itemNameText.text = item.itemName;
        priceText.text = item.price.ToString() + " monedas";
        buyButton.onClick.AddListener(BuyItem);
    }

    public void BuyItem()
    {
        if (itemData != null)
        {
            Debug.Log("Compraste: " + itemData.itemName);

            // Verifica si el ítem tiene un efecto y lo aplica
            if (itemData.effect != null)
            {
                itemData.effect.Apply(); // Aplica el efecto directamente sin switch
            }

            // Lógica adicional de compra
            GameState.Instance.coinsAccumulated -= 10; // Resta el precio del ítem
            itemData.isVisible = false; // Puede ser útil para deshabilitar el ítem visualmente
            SaveItemPurchase();
        }
    }

    void SaveItemPurchase()
    {
        // Guardar que el ítem ha sido comprado
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { itemData.itemName, true }
        };
        Save.SaveData(data);
    }
    */
}
