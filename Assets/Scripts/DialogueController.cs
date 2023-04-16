using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour, IDialogueController
{
    public TMP_Text text;
    public string[] possibleRequest;

    private int currentIndex;

    public List<Button> buttons;

    public float timeWrite;
    private bool isTyping = false;


    public ButtonsController buttonController;

    


    private void Start()
    {
       ShowRandomRequest();
    }

    public void Update()
    {
        
    }

    public void ShowRandomRequest()
    {
        currentIndex = Random.Range(0, possibleRequest.Length);
        string selectedText = possibleRequest[currentIndex];
        StartCoroutine(TypeRequest(selectedText));
    }


    private IEnumerator TypeRequest(string request)
    {
        isTyping = true;
        text.text = "";
        buttonController.DisableOrActive(false);
        

        foreach (char c in request)
        {
            text.text += c;
            yield return new WaitForSeconds(timeWrite);
        }

        buttonController.DisableOrActive(true);
        isTyping = false;
    }

   

}
