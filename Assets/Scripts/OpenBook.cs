using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OpenBook : MonoBehaviour
{
    [SerializeField] Button openBtn;

    private Vector3 rotationVector;

    private bool isOpenCliked;

    private DateTime startTime;
    private DateTime endTime;

    private void Start()
    {
        if (openBtn != null)
        {
            openBtn.onClick.AddListener(() => openBtn_Click());
        }
    }

    public void Update()
    {
        if (isOpenCliked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if ((endTime - startTime).TotalSeconds >= 1)
            {
                isOpenCliked = false;
            }
        }
    }

    private void openBtn_Click()
    {
        isOpenCliked = true;
        startTime = DateTime.Now;

        rotationVector = new Vector3(0, 180, 0);
    }
}
