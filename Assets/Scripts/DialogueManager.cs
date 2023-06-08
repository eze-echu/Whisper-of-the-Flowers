using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject textObjectPrefab;
    public RectTransform contentTransform;
    [SerializeField] RectTransform _scrollView;
    public float maxTextWidth = 600f;

    public RectTransform desrieX;

    private static DialogueManager instance;

    public void Start()
    {

        ShowRequest("Inserte Texto");

    }

    private DialogueManager(RectTransform contentTransform, GameObject textObjectPrefab)
    {
        this.contentTransform = contentTransform;
        this.textObjectPrefab = textObjectPrefab;
    }

    public static DialogueManager GetInstance(RectTransform contentTransform, GameObject textObjectPrefab)
    {
        if (instance == null)
        {
            instance = new DialogueManager(contentTransform, textObjectPrefab);
        }
        return instance;
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
        AdjustObjectBackgroundWidth(newTextObject);
        //AdjustObjectPosition(newTextObject);
        //AdjustContentSize();
        //ScrollToBottom();


    }

    private void AdjustObjectBackgroundWidth(GameObject newTextObject)
    {

        /*
        RectTransform newTextRect = newTextObject.GetComponent<RectTransform>();
        TextMeshProUGUI textMesh = newTextObject.GetComponentInChildren<TextMeshProUGUI>();

        // Obtener el ancho preferido del texto
        float textWidth = textMesh.preferredWidth;

        // Obtener el ancho máximo permitido para el objeto instanciado
        float maxObjectWidth = _scrollView.GetComponent<RectTransform>().rect.width;

        // Calcular el nuevo valor del borde izquierdo (left) del objeto
        float newLeft = Mathf.Clamp(textWidth, 0f, maxObjectWidth) * 0.5f; // La mitad del ancho del texto

        // Establecer el nuevo valor del borde izquierdo (left) del objeto
        Vector2 offsetMin = newTextRect.offsetMin;
        offsetMin.x = -newLeft;
        newTextRect.offsetMin = offsetMin;
        */

        RectTransform backgroundRect = newTextObject.GetComponentInChildren<Image>().rectTransform;
        TextMeshProUGUI textMesh = newTextObject.GetComponentInChildren<TextMeshProUGUI>();

        // Obtener el ancho preferido del texto
        float textWidth = textMesh.preferredWidth;

        // Obtener el ancho máximo permitido para el objeto instanciado
        float maxObjectWidth = _scrollView.GetComponent<RectTransform>().rect.width;

        // Calcular el nuevo valor del borde izquierdo (left) del objeto
        float newLeft = Mathf.Clamp(textWidth, 0f, maxObjectWidth) * 0.5f; // La mitad del ancho del texto

        // Establecer el nuevo valor del borde izquierdo (left) del objeto
        Vector2 offsetMin = backgroundRect.offsetMin;
        offsetMin.x = -newLeft;
        backgroundRect.offsetMin = offsetMin;

    }

    private void AdjustObjectPosition(GameObject newTextObject)
    {
        
        RectTransform newTextRect = newTextObject.GetComponent<RectTransform>();
        RectTransform contentRect = contentTransform.GetComponent<RectTransform>();

        newTextRect.SetParent(contentRect);
        newTextRect.localScale = Vector3.one;

        // Establecer la posición del newTextObject en el borde derecho del contentTransform
        Vector2 anchoredPosition = newTextRect.anchoredPosition;
        float newX = desrieX.anchoredPosition.x ;
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
