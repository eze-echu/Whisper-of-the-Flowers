using System.Collections;
using System.Collections.Generic;
using Flowers;
using Unity.VisualScripting;
using UnityEngine;
using Bouquet = Racimo.Bouquet;

public class InteractiveReDo : TouchInteraction
{
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
            GameManager.instance.AM.PlayEffect(EffectSound);
            return (hit.collider.gameObject == gameObject);
        }

        return false;
    }

    public override void ToggleObjectActivation(){
        if (_target != null)
        {
            Bouquet.Instance.ResetToOriginalState();
            FlowerHandler.instance.ResetWorkspace();
            CameraController.instance.SwitchToSpecificCamera(workstation: Bouquet.Workstations.VaseStation);
        }
    }
}
