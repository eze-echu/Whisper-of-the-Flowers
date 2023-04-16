using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public List<Button> buttons;

   

    public void DisableOrActive(bool f)
    {
        foreach (Button button in buttons)
        {
            button.interactable = f;
        }
    }
}
