using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class ToolTipController : MonoBehaviour
{
    public static ToolTipController Instance;

    private RectTransform tooltipRect;

    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset = new Vector2(15f, -15f);

    private bool isTooltipVisible = false;

    private void Awake()
    {
        Instance = this;
        tooltipRect = tooltipPanel.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1); // ← esquina superior izquierda
        tooltipPanel.SetActive(false);
    }

    private void Start()
    {
        var layout = tooltipText.GetComponent<LayoutElement>();
        if (layout != null)
        {
            layout.preferredWidth = 300f;
            layout.flexibleHeight = 1;
        }

        tooltipText.enableWordWrapping = true;
        //tooltipText.overflowMode = TextOverflowModes.overflowMode;
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 mousePos = Input.mousePosition;
            tooltipPanel.transform.position = mousePos + offset;
        }
    }

    public void ShowTooltip(string text)
    {
        /*
        if (isTooltipVisible && tooltipText.text == text)
            return; // ya visible con el mismo texto, no hacer nada

        tooltipText.text = text;
        tooltipPanel.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipPanel.GetComponent<RectTransform>());
        isTooltipVisible = true;
        */

        if (isTooltipVisible && tooltipText.text == text)
        return;

        tooltipText.text = text;
        tooltipPanel.SetActive(true);

        // Esperamos un frame para que se actualice el texto antes de calcular tamaño
        StartCoroutine(AdjustPanelToText());
        isTooltipVisible = true;

    }

    public void HideTooltip()
    {
        if (!isTooltipVisible)
            return; // ya está oculto, no hacer nada

        tooltipPanel.SetActive(false);
        isTooltipVisible = false;
    }

    private IEnumerator AdjustPanelToText()
    {
        yield return null; // espera un frame para que TMP calcule el tamaño correctamente

        float padding = 20f; // espacio alrededor del texto
        float maxWidth = 300f;

        float textWidth = Mathf.Min(tooltipText.preferredWidth, maxWidth);
        float textHeight = tooltipText.preferredHeight;

        tooltipRect.sizeDelta = new Vector2(textWidth + padding, textHeight + padding);
    }
}
