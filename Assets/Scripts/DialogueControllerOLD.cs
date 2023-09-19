using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueControllerOLD : MonoBehaviour, IDialogueController
{
    public TMP_Text text;
    public string[] additionalRequest;

    private int currentIndex;
    private bool skiped = false;
    public List<Button> buttons;

    public float timeWrite;
    private Coroutine currentCoroutine;
    private bool isTyping = false;


    public ButtonsController buttonController;

    public CanvasGroup endDisplay;

    public HandInZone handInZone;

  

    public void ShowSpecificRequest(string text, bool end = false)
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

        currentCoroutine = StartCoroutine(TypeRequest(text, end));
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

    private IEnumerator TypeRequest(string request, bool end = false)
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
        handInZone.tag = "Occupied";
        text.text = "";
        GameManager.Trigger("DisableOrActiveButtons");
        if (end)
        {
            FindObjectOfType<FlowerHandler>().DisableAllFlowers();
            //endDisplay.blocksRaycasts = true;
        }

        foreach (char c in request)
        {
            if (skiped)
            {
                skiped = false;
                text.text = request;
                break;
            }
            text.text += c;
            yield return new WaitForSeconds(timeWrite);
        }
        if (end)
        {
            StartCoroutine(End());
        }
        GameManager.Trigger("DisableOrActiveButtons");
        handInZone.tag = "DropZone";
        isTyping = false;
    }

    public void skip()
    {
        if (isTyping)
        {
            skiped = true;
        }
    }

    private IEnumerator End()
    {
        int time = 3;
        float elapsedTime = 0;

        yield return new WaitForSeconds(5);

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            endDisplay.alpha = Mathf.Lerp(0, 1, elapsedTime / time);
            yield return null;
        }
        endDisplay.blocksRaycasts = true;
        endDisplay.interactable = true;
    }
}
