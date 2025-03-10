using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemClickHandler : MonoBehaviour
{
    private Button itemButton;
    private string itemName;
    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    private bool alreadyBought = false;

    public void Setup(Button button, string itemName, bool alreadyBought)
    {
        this.itemButton = button;
        this.itemName = itemName;
        this.alreadyBought = alreadyBought;

        if (alreadyBought)
        {
            itemButton.interactable = false;
        }
        else
        {
            itemButton.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        if (alreadyBought) return;

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            Buying();
        }
        lastClickTime = Time.time;
    }

    void Buying()
    {
        Debug.Log($"Compraste: {itemName}");
        alreadyBought = true;
        itemButton.interactable = false;

        // Guardar la compra
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { itemName, true }
        };
        Save.SaveData(data);
    }
}
