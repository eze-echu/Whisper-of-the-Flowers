using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveExitObject : TouchInteraction
{
    //public string levelToGo;
    public PopupConfirmation Pc;
    public string textPopUp;

    public override void ToggleObjectActivation()
    {
        //GameManager.instance.Fc.FadeToLevel(levelToGo);
        Pc.ShowPopup(textPopUp);
    }
}
