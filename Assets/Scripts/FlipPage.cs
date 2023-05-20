using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FlipPage : MonoBehaviour
{
    private enum ButtonType
    {
        NextButton,
        Prevbutton
    }


    [SerializeField] Button nextBtn;
    [SerializeField] Button prevBtn;
    [SerializeField] Button closeBtn;

    //si se cambian a Text hay que cambiar y ajustar todo texto puesto en la escena
    [SerializeField] TextMeshProUGUI headerText1_1;
    [SerializeField] TextMeshProUGUI headerText1_2;
    [SerializeField] TextMeshProUGUI headerText2_1;
    [SerializeField] TextMeshProUGUI headerText2_2;

    [SerializeField] TextMeshProUGUI bodyText1_1;
    [SerializeField] TextMeshProUGUI bodyText1_2;
    [SerializeField] TextMeshProUGUI bodyText2_1;
    [SerializeField] TextMeshProUGUI bodyText2_2;

    [SerializeField] TextMeshProUGUI footerText1_1;
    [SerializeField] TextMeshProUGUI footerText1_2;
    [SerializeField] TextMeshProUGUI footerText2_1;
    [SerializeField] TextMeshProUGUI footerText2_2;


    private Vector3 rotationVector;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isOpenCliked;

    private DateTime startTime;
    private DateTime endTime;

 

    private void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;

        if (nextBtn != null)
        {
            nextBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.NextButton));
        }

        if (prevBtn != null)
        {
            prevBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.Prevbutton));
        }

        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(() => closeBookBtn_Click());
        }
    }

    private void Update()
    {
        if (isOpenCliked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);

            endTime = DateTime.Now;
            if ((endTime - startTime).TotalSeconds >= 1)
            {
                isOpenCliked = false;
                transform.rotation = startRotation;
                transform.position = startPosition;

                SetVisibleText();
            }
        }
    }

    private void OnEnable()
    {
        AppEvents.OpenBookEvent.AddListener(openBookBtn_Click);
    }

    private void OnDisable()
    {
        AppEvents.OpenBookEvent.RemoveListener(openBookBtn_Click);
    }


    private void turnOnePageBtn_Click(ButtonType type)
    {
        isOpenCliked = true;
        startTime = DateTime.Now;

        nextBtn.gameObject.SetActive(true);
        prevBtn.gameObject.SetActive(true);

        if (type == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, -180, 0);

            SetFlipPageText(Page.CurrentPage2, Page.CurrentPage2 + 1);

            Page.CurrentPage1 += 2;
            Page.CurrentPage2 += 2;
            Page pge = Page.RandomPage;

            //SetVisibleText();

            if ((Page.CurrentPage2 >= pge.Pages.Count) || (Page.CurrentPage1 >= pge.Pages.Count))
            {
                nextBtn.gameObject.SetActive(false);
            }
        }
        else if (type == ButtonType.Prevbutton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.z);
            transform.rotation = Quaternion.Euler(newRotation);

            rotationVector = new Vector3(0, 180, 0);

            SetFlipPageText(Page.CurrentPage1 - 1, Page.CurrentPage1);

            Page.CurrentPage1 += 2;
            Page.CurrentPage2 += 2;

            if ((Page.CurrentPage2 <= 0) || (Page.CurrentPage1 <= 0))
            {
                prevBtn.gameObject.SetActive(false);
            }
        }

    }

    //ACA
    private void openBookBtn_Click()
    {
        print("open book cliked in flipbook.cs");
        Page pge = Page.GetRandomPage();

        Page.CurrentPage1 = 0;
        Page.CurrentPage2 = 1;

        prevBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);

        if (pge.Pages.Count > 2)
        {
            nextBtn.gameObject.SetActive(true);
        }

        SetVisibleText();
    }

    private void SetVisibleText()
    {
        Page pge = Page.RandomPage;

        string footer1 = "";
        string footer2 = "";
        string body1 = "";
        string body2 = "";
        string header1 = "";
        string header2 = "";

        if (Page.CurrentPage1 < pge.Pages.Count)
        {
            footer1 = String.Format("Page {0} of {1}", Page.CurrentPage1 + 1, pge.Pages.Count);
            body1 = pge.Pages[Page.CurrentPage1];
            header1 = pge.Title;
        }
        if (Page.CurrentPage2 < pge.Pages.Count)
        {
            footer2 = String.Format("Page {0} of {1}", Page.CurrentPage2 + 1, pge.Pages.Count);
            body2 = pge.Pages[Page.CurrentPage2];
            header2 = pge.Title;
        }

        headerText1_1.text = header1;
        headerText2_1.text = header1;

        footerText1_1.text = footer1;
        footerText2_1.text = footer2;

        print(body1 + " " + body2);
        bodyText1_1.text = body1;
        bodyText2_1.text = body1;
    }

    private void SetFlipPageText(int leftPage, int rightPage)
    {
        Page pge = Page.RandomPage;

        string footerR = "";
        string footerL = "";
        string bodyR = "";
        string bodyL = "";
        string headerR = "";
        string headerL = "";

        if (rightPage < pge.Pages.Count)
        {
            footerR = String.Format("Page {0} of {1}", rightPage + 1, pge.Pages.Count);
            bodyR = pge.Pages[rightPage];
            headerR = pge.Title;
        }
        if (leftPage < pge.Pages.Count)
        {
            footerL = String.Format("Page {0} of {1}", leftPage + 1, pge.Pages.Count);
            bodyL = pge.Pages[leftPage];
            headerL = pge.Title;
        }

        headerText1_2.text = headerL;
        headerText2_2.text = headerR;

        footerText1_2.text = footerL;
        footerText2_2.text = footerR;

        bodyText1_2.text = bodyL;
        bodyText2_2.text = bodyR;
    }

    private void closeBookBtn_Click()
    {
        AppEvents.InvokeCloseBookEvent();
    }
}
