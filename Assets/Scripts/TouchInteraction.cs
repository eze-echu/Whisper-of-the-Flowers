using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchInteraction : MonoBehaviour
{
    [SerializeField] GameObject _target;

   


    private void OnMouseDown()
    {
        if (CheckClickedObject())
        {
            ToggleObjectActivation();
        }
    }

    private bool CheckClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return (hit.collider.gameObject == gameObject);
        }

        return false;
    }

    public void ToggleObjectActivation()
    {
        if (_target != null)
        {
            if(_target.GetComponent<CanvasGroup>()){
                var a = _target.GetComponent<CanvasGroup>();
                a.alpha = Convert.ToInt64(!Convert.ToBoolean(a.alpha));
                a.interactable = !a.interactable;
                a.blocksRaycasts = !a.blocksRaycasts;
            }
            else{
                _target.SetActive(!_target.activeSelf);
            }
        }

    }
}
