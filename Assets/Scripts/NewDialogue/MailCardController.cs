using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MailCardController : TouchInteraction
{
    public static MailCardController instance;
    
    private void Start(){
        GameManager.Subscribe("OnCameraChange", OnCameraChange);
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("OnCameraChange", OnCameraChange);
    }
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public override void ToggleObjectActivation(){
        if (_target != null)
        {
            EffectSound.Play();
            if(_target.GetComponent<CanvasGroup>()){
                var a = _target.GetComponent<CanvasGroup>();
                a.alpha = Convert.ToInt64(!Convert.ToBoolean(a.alpha));
                a.interactable = !a.interactable;
                a.blocksRaycasts = !a.blocksRaycasts;
            }
        }
    }
    public void DisableObject(){
        var a = _target.GetComponent<CanvasGroup>();
        a.alpha = 0;
        a.interactable = false;
        a.blocksRaycasts = false;
    }
    public void EnableObject(){
        var a = _target.GetComponent<CanvasGroup>();
        a.alpha = 1;
        a.interactable = true;
        a.blocksRaycasts = true;
    }
    private void OnCameraChange(){
        //Debug.LogError("camera changed");
        if(CameraController.instance.currentCameraIndex == 1){
            DisableObject();
        }
        else if(CameraController.instance.currentCameraIndex == 0){
            EnableObject();
        }
    }
}
