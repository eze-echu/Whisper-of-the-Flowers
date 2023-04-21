using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour, IDialogueController
{
    public TMP_Text text;
    public string[] additionalRequest;

    private int currentIndex;

    public List<Button> buttons;

    public float timeWrite;
    private Coroutine currentCoroutine;
    private bool isTyping = false;


    public ButtonsController buttonController;

    


    private void Start()
    {
       //ShowRandomRequest();
    }

    public void ShowSpecificRequest(string text)
    {
        //StartCoroutine(TypeRequest(text));

        /*
        if (additionalRequest.Length > 0)
        {
            currentIndex = Random.Range(0, additionalRequest.Length);
            string selectedText = additionalRequest[currentIndex];
            StartCoroutine(TypeRequest(selectedText));


        }
        */

        if (isTyping)
        {
            StopCoroutine(currentCoroutine);
            isTyping = false;
        }

        currentCoroutine = StartCoroutine(TypeRequest(text));
    }

    public void ShowRandomRequest()
    {
        if (additionalRequest.Length > 0)
        {
            if (isTyping)
            {
                StopCoroutine(currentCoroutine);
                isTyping = false;
            }

            currentIndex = Random.Range(0, additionalRequest.Length);
            string selectedText = additionalRequest[currentIndex];
            currentCoroutine = StartCoroutine(TypeRequest(selectedText));
        }
    }

    private IEnumerator TypeRequest(string request)
    {
        /*
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
        */

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
