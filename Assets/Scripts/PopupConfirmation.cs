using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupConfirmation : MonoBehaviour
{
    public TextMeshProUGUI confirmationText;
    public GameObject Background;
    public string levelToGo;

    public void ShowPopup(string message)
    {
        Background.SetActive(true);
        confirmationText.text = message;
    }

    public void ConfirmAction()
    {
        // Lógica a ejecutar cuando se confirma la acción.
        // Por ejemplo, cambiar de escena.
        //SceneManager.LoadScene("NuevaEscena");
        GameManager.instance.Fc.FadeToLevel(levelToGo);
    }

    public void CancelAction()
    {
        // Lógica a ejecutar cuando se cancela la acción.
        Background.SetActive(false);
    }
}
