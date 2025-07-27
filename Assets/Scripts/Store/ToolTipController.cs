using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class ToolTipController : MonoBehaviour
{
    public static ToolTipController Instance;

    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset = new Vector2(15f, -15f);

    private bool isTooltipVisible = false;

    private void Awake()
    {
        Instance = this;
        tooltipPanel.SetActive(false);
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
        if (isTooltipVisible && tooltipText.text == text)
            return; // ya visible con el mismo texto, no hacer nada

        tooltipText.text = text;
        tooltipPanel.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipPanel.GetComponent<RectTransform>());
        isTooltipVisible = true;
    }

    public void HideTooltip()
    {
        if (!isTooltipVisible)
            return; // ya está oculto, no hacer nada

        tooltipPanel.SetActive(false);
        isTooltipVisible = false;
    }
}
