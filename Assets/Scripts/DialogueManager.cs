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
        ShowRequest("pipo");
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
        GameObject newTextObject = Instantiate(textObjectPrefab, contentTransform);
        TextMeshProUGUI newText = newTextObject.GetComponentInChildren<TextMeshProUGUI>();
        newText.text = text;

        // Ajustar el ancho del fondo según el tamaño del texto
        RectTransform textRect = newText.GetComponent<RectTransform>();
        RectTransform backgroundRect = newTextObject.GetComponent<RectTransform>();

        float textWidth = textRect.sizeDelta.x;
        if (textWidth > maxTextWidth)
        {
            float scaleFactor = maxTextWidth / textWidth;
            textRect.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }

        // Ajustar el tamaño del Content Size Fitter para mostrar el nuevo texto
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform.GetComponent<RectTransform>());

        // Asegurarse de que el ScrollView se desplace hasta el final para mostrar el nuevo texto
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
