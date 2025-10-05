using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class TutorialPopUp : MonoBehaviour
    {
        [SerializeField] private Sprite[] OrderedImages;
        private uint index = 0;
        [SerializeField] private Image DisplayImage;

        public void Next()
        {
            index++;
            if (index >= OrderedImages.Length)
            {
                index = 0;
            }

            DisplayImage.sprite = OrderedImages[(int)index];
        }

        public void Previous()
        {
            if (index == 0)
            {
                index = (uint)OrderedImages.Length - 1;
            }
            else
            {
                index--;
            }

            DisplayImage.sprite = OrderedImages[(int)index];
        }

        void Awake()
        {
            DisplayImage.sprite = OrderedImages[index];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}