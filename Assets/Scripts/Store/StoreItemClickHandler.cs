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
    public AudioClip buySound; 
    public AudioClip errorSound;
    public AudioSource audioSource;

    public void Start()
    {

    }
   public void Setup(Button button, StoreItem item, bool alreadyBought)
    {
        this.itemButton = button;
        this.itemData = item;  // Guardamos el StoreItem completo
        this.itemName = item.itemName;
        this.alreadyBought = alreadyBought;

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

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
           if(GameState.Instance.coinsAccumulated >= itemData.price)
            {
                Buying();
            }
            else
            {
                if (audioSource && errorSound) audioSource.PlayOneShot(errorSound);
            }
        }
        lastClickTime = Time.time;
    }

    void Buying()
    {
        Debug.Log($"Compraste: {itemName}");
        if (audioSource && buySound) audioSource.PlayOneShot(buySound);

        // Realiza la compra (restar monedas, etc.)
        GameState.Instance.coinsAccumulated -= itemData.price;
        alreadyBought = true;
        itemButton.interactable = false;
        
        TextMeshProUGUI textComponent = itemButton.GetComponentInChildren<TextMeshProUGUI>();

        if (textComponent != null)
        {
            textComponent.text = "Sold Out"; 
            
            textComponent.alignment = TextAlignmentOptions.Center; 
            
            // textComponent.color = Color.red; 
            textComponent.transform.localPosition += new Vector3(1.5f, 1.5f, 0f);

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
