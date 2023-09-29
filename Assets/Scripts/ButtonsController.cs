using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public List<Button> buttons;

    private void Start(){
        GameManager.Subscribe("DisableOrActiveButtons", DisableOrActiveButtons);
        GameManager.Trigger("DisableOrActiveButtons");
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("DisableOrActiveButtons", DisableOrActiveButtons);
    }

    public void DisableOrActiveButtons()
    {
        foreach (Button button in buttons)
        {
            if(button != null){
                button.interactable = !button.interactable;
            }
        }
    }
}
