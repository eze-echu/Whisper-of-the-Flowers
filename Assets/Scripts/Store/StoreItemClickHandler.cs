using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Maneja los clics en los ítems, permitiendo la compra con doble clic.
/// </summary>
public class StoreItemClickHandler : MonoBehaviour
{
    
    private Button itemButton;
    private string itemName;
    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    private StoreItem itemData;  
    private bool alreadyBought = false;


   public void Setup(Button button, StoreItem item, bool alreadyBought)
    {
        this.itemButton = button;
        this.itemData = item;  // Guardamos el StoreItem completo
        this.itemName = item.itemName;
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

        // Realiza la compra (restar monedas, etc.)
        GameState.Instance.coinsAccumulated -= 10;
        alreadyBought = true;
        itemButton.interactable = false;
        
        TextMeshProUGUI textComponent = itemButton.GetComponentInChildren<TextMeshProUGUI>();

        if (textComponent != null)
        {
            // Cambiamos el texto
            textComponent.text = "Sold Out"; 
            
            // Centramos el texto (Horizontal y Verticalmente)
            textComponent.alignment = TextAlignmentOptions.Center; 
            
            // Le damos color rojo (opcional, queda bien para "Sold Out")
            // textComponent.color = Color.red; 

            // Aplicamos la rotación de 30 grados en el eje Z
            // Quaternion.Euler(x, y, z)
            textComponent.transform.localRotation = Quaternion.Euler(0, 0, 30);
            
            // Opcional: Aumentar un poco la fuente si se ve chico
            // textComponent.fontSize += 10; 
        }

        // Guardar la compra
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { itemName, true }
        };
        Save.SaveData(data);

        // Aplicar el efecto
        if (itemData.effect != null)
        {
            itemData.effect.Apply();  // Aquí llamamos al efecto del ítem
        }
    }

    
}
