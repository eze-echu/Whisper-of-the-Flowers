using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchInteraction : MonoBehaviour
{
    [SerializeField] GameObject _target;

    [SerializeField] GameObject _Background;

   


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
            _target.SetActive(!_target.activeSelf);
        }

        if (_Background != null)
        {
            _Background.SetActive(!_Background.activeSelf);
        }
    }
}
