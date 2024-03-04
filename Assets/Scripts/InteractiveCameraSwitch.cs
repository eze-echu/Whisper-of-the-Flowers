using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractiveCameraSwitch : TouchInteraction
{
    public int cameraToSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool CheckClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return (hit.collider.gameObject == gameObject);
        }

        return false;
    }

    public override void ToggleObjectActivation(){
        if (_target != null)
        {
            EffectSound.Play();
            GameManager.Trigger("OnCameraChange");
            CameraController.instance.SwitchToSpecificCamera(cameraToSwitch);
        }
    }
}
