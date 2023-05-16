using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
            }
        }
    }

    private void turnOnePageBtn_Click(ButtonType type)
    {
        isOpenCliked = true;
        startTime = DateTime.Now;

        if (type == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, -180, 0);
        }
        else if (type == ButtonType.Prevbutton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.z);
            transform.rotation = Quaternion.Euler(newRotation);

            rotationVector = new Vector3(0, 180, 0);
        }

    }

    private void closeBookBtn_Click()
    {
        AppEvents.InvokeCloseBookEvent();
    }
}
