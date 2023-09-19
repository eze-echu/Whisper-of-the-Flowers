using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameStateManager : MonoBehaviour
{
    public GameObject handInZone;
    public ButtonsController buttonController;
    public CanvasGroup endDisplay;
    public TMP_Text EndMessage;
    public static GameStateManager instance;
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        GameManager.Subscribe("DisableWorkspace", DisableWorkspace);
        GameManager.Subscribe("EnableWorkspace", EnableWorkspace);
        GameManager.Subscribe("EndChapter", EndChapter);
        //GameManager.Trigger("ToggleDragAndDrop");
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("DisableWorkspace", DisableWorkspace);
        GameManager.Unsuscribe("EnableWorkspace", EnableWorkspace);
        GameManager.Unsuscribe("EndChapter", EndChapter);
    }

    private void DisableWorkspace(){
        GameManager.Trigger("DisableOrActiveButtons");
        handInZone.tag = "Occupied";
        print("Workspace Disabled");
    }

    private void EnableWorkspace(){
        GameManager.Trigger("DisableOrActiveButtons");
        handInZone.tag = "DropZone";
        print("Workspace Enabled");
    }
    private void EndChapter(){
        print("Ending Chapter");
        DisableWorkspace();
        StartCoroutine(End());
    }
    private IEnumerator End(){
        int time = 3;
        float elapsedTime = 0;
        endDisplay.blocksRaycasts = true;
        // foreach(var a in FindObjectsOfType<DragAndDrop>()){
        //     a.dragable
        // }

        yield return new WaitForSeconds(5);

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            endDisplay.alpha = Mathf.Lerp(0, 1, elapsedTime / time);
            yield return null;
        }
        endDisplay.interactable = true;
    }
}
