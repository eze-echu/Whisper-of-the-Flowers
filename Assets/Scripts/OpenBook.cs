using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OpenBook : MonoBehaviour
{
    [SerializeField] Button openBtn;

    [SerializeField] GameObject openedBook;
    [SerializeField] GameObject insideBackCover;

    private Vector3 rotationVector;

    private bool isOpenCliked;
    private bool isCloseClicked;

    private DateTime startTime;
    private DateTime endTime;


  
    private void Start()
    {
        if (openBtn != null)
        {
            openBtn.onClick.AddListener(() => openBtn_Click());
        }

        AppEvents.CloseBookEvent.AddListener(closeBook_Click);
        AppEvents.OpenBookEvent.AddListener(OpenBookEventInvoked);
    }

    public void Update()
    {
        if (isOpenCliked || isCloseClicked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if (isOpenCliked)
            {
                if ((endTime - startTime).TotalSeconds >= 1)
                {
                    isOpenCliked = false;
                    gameObject.SetActive(false);
                    insideBackCover.SetActive(false);
                    openedBook.SetActive(true);

                    //ACA
                    AppEvents.OpenBookEvent.Invoke();
                }
            }
            if (isCloseClicked)
            {
                if ((endTime - startTime).TotalSeconds >= 1)
                {
                    isCloseClicked = false;
                }
            }
           
        }
    }

    private void openBtn_Click()
    {
        isOpenCliked = true;
        startTime = DateTime.Now;

        rotationVector = new Vector3(0, 180, 0);
    }

    
    private void closeBook_Click()
    {
        gameObject.SetActive(true);
        openedBook.SetActive(false);
        insideBackCover.SetActive(true);

        isCloseClicked = true;
        startTime = DateTime.Now;
        rotationVector = new Vector3(0, -180, 0);
        print("close book clicked in Openbook");
    }

    private void OpenBookEventInvoked()
    {
        //ACA - Handle OpenBookEvent
        //print("open book clicked in OpenBook");
    }
}
