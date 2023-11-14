using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveExitObject : TouchInteraction
{
    public string levelToGo;

    public override void ToggleObjectActivation()
    {
        GameManager.instance.Fc.FadeToLevel(levelToGo);
    }
}
