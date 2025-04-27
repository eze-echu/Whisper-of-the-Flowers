using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class TouchInteraction : MonoBehaviour
{
    [SerializeField] public GameObject _target;

    public AudioSource EffectSound;

    [SerializeField] private bool stopsTime = false;


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
            if (stopsTime && _target.activeSelf)
            {
                GameState.PauseGame();
            }
            else
            {
                GameState.ResumeGame();
            }
        }

    }

    
}
