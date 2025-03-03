using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;

    private StoreItem itemData;

    public void Setup(StoreItem item)
    {
        itemData = item;
        itemNameText.text = item.itemName;
        priceText.text = item.price.ToString() + " monedas";
        buyButton.onClick.AddListener(BuyItem);
    }

    void BuyItem()
    {
        Debug.Log("Compraste: " + itemData.itemName);
        // Aquí puedes agregar la lógica de compra
    }
}
