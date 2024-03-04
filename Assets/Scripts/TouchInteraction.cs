using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchInteraction : MonoBehaviour
{
    [SerializeField] public GameObject _target;

    public AudioSource EffectSound;


    private void OnMouseDown()
    {
        if (CheckClickedObject())
        {
            ToggleObjectActivation();
        }
    }

    public virtual bool CheckClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return (hit.collider.gameObject == gameObject);
        }

        return false;
    }

    public virtual void ToggleObjectActivation()
    {
        if (_target != null)
        {
            if (EffectSound != null) EffectSound.Play();
  
            _target.SetActive(!_target.activeSelf);
        }

    }

    
}
