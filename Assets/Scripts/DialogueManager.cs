using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject textObjectPrefab;
    public Transform contentTransform;
    public float maxTextWidth = 600f;

    public void Start()
    {
        ShowRequest("pipopipo");
    }


    public void ShowRequest<T>(T value)
    {
        if (value is string stringvalue)
        {
            CreateTextObject(stringvalue);
        }
        else
        {
            string convertedValue = ConvertValueToString(value);
            if (convertedValue != null)
            {
                // Continuar con la ejecución para el valor convertido a string
                CreateTextObject(convertedValue);
            }
            else
            {
                Debug.LogError("Tipo de valor no soportado");
            }
        }
    }

    private string ConvertValueToString<T>(T value)
    {
        try
        {
            return value.ToString();
        }
        catch
        {
            return null;
        }
    }

    private void CreateTextObject(string text)
    {
        /*
        GameObject newTextObject = Instantiate(textObjectPrefab, contentTransform);
        TextMeshProUGUI textMesh = newTextObject.GetComponentInChildren<TextMeshProUGUI>();
        RectTransform newTextRect = newTextObject.GetComponent<RectTransform>();

        textMesh.text = text;

        // Ajustar el ancho del objeto vacío según el tamaño del texto
        float textWidth = textMesh.preferredWidth;
        float objectWidth = Mathf.Min(textWidth, maxTextWidth); // Limitar el ancho del objeto al máximo permitido
        Vector2 objectSize = newTextRect.sizeDelta;
        objectSize.x = objectWidth;
        newTextRect.sizeDelta = objectSize;

        // Ajustar el tamaño del Content Size Fitter para mostrar el nuevo texto
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform.GetComponent<RectTransform>());

        // Asegurarse de que el ScrollView se desplace hasta el final para mostrar el nuevo texto
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0, 0);

        // Restaurar el tamaño original del texto
        textMesh.rectTransform.sizeDelta = new Vector2(0f, textMesh.rectTransform.sizeDelta.y);
        */
        GameObject newTextObject = Instantiate(textObjectPrefab, contentTransform);
        TextMeshProUGUI textMesh = newTextObject.GetComponentInChildren<TextMeshProUGUI>();

        textMesh.text = text;

        AdjustObjectPosition(newTextObject);
        AdjustBackgroundWidth(newTextObject, textMesh);
        //AdjustContentSize();
        //ScrollToBottom();


    }

    private void AdjustBackgroundWidth(GameObject newTextObject, TextMeshProUGUI textMesh)
    {
        RectTransform backgroundRect = newTextObject.GetComponentInChildren<Image>().rectTransform;
        float textWidth = textMesh.preferredWidth;
        float backgroundWidth = Mathf.Min(textWidth, maxTextWidth);
        Vector2 backgroundSize = backgroundRect.sizeDelta;
        backgroundSize.x = backgroundWidth;
        backgroundRect.sizeDelta = backgroundSize;
    }

    private void AdjustObjectPosition(GameObject newTextObject)
    {
        RectTransform newTextRect = newTextObject.GetComponent<RectTransform>();
        RectTransform contentRect = contentTransform.GetComponent<RectTransform>();

        newTextRect.SetParent(contentRect);
        newTextRect.localScale = Vector3.one;

        // Establecer la posición del newTextObject en el borde derecho del contentTransform
        Vector2 anchoredPosition = newTextRect.anchoredPosition;
        float newX = (contentRect.rect.width * 0.5f) + (newTextRect.rect.width ); // Calcular la posición X
        float newY = (contentRect.rect.height * 0.5f) - (newTextRect.rect.height * 0.5f); // Calcular la posición Y
        anchoredPosition.x = newX;
        anchoredPosition.y = newY;
        newTextRect.anchoredPosition = anchoredPosition;

       
    }

    private void AdjustContentSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform.GetComponent<RectTransform>());
    }

    private void ScrollToBottom()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
