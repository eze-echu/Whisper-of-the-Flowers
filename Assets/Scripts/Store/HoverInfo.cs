using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInfo: MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    public StoreItem storeItem;

       public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipController.Instance.ShowTooltip(storeItem.infoItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.Instance.HideTooltip();
    }
}
