using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Systems
{
    public class EOD : MonoBehaviour
    {
        private static readonly int FadeInAndOut = Animator.StringToHash("FadeInAndOut");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        public Animator animator;
        private string _sceneName;
        public TextMeshProUGUI texto;
        [SerializeField] private GameObject familyMemberDisplay;

        [Serializable]
        public class FamilyMemberOptions
        {
            [SerializeField] internal Toggle medsSelected;
            [SerializeField] internal Toggle foodSelected;
            [SerializeField] internal TextMeshProUGUI amounTextMeshProUGUI;
        }

        [SerializeField] private List<FamilyMemberOptions> FamilyCheckBoxes;

        public static EOD Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void Start()
        {
            if (GameManager.instance.EODFS == null) GameManager.instance.EODFS = this;
            else Destroy(gameObject);
        }

        public void FadeToLevel(string scene)
        {
            texto.text = "";
            _sceneName = scene;
            animator.SetTrigger(FadeOut);
            SceneLoader.Instance().AsyncLoadScene(_sceneName);
        }

        /*
        public void FadeInAndOut(string Message)
        {
            texto.text = Message;

        }
        */

        public IEnumerator FadeInAndOutCoroutine(string message)
        {
            // Setea el mensaje
            texto.text = message;
            GameState.PauseGame();
            print("entro");
            // Inicia la animaci�n
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
            animator.Update(0f);


            animator.SetTrigger(FadeInAndOut);

            // Espera a que termine la animaci�n
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            GameState.ResumeGame();
            // Limpia el mensaje y carga la siguiente escena
            //texto.text = "";
        }

        public IEnumerator StartFadeIn(string message)
        {
            //texto.text = message;
            ChangeEODText(message);
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, layer: -1, normalizedTime: 0f);
            animator.Update(Time.deltaTime);
            GameState.PauseGame();

            animator.SetBool(FadeOut, true);


            yield return new WaitForSeconds(1);
        }

        public void ChangeEODText(string message)
        {
            texto.text = message;
        }

        public IEnumerator StartFadeOut()
        {
            animator.SetBool(FadeOut, false);
            animator.SetBool(FadeIn, true);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetBool(FadeIn, false);

            GameState.ResumeGame();
        }


    }
}