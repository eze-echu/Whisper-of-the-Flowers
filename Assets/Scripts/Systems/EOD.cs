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
            [SerializeField] internal Family.Relationships relationship;
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
            ResetTogglesToFalse();

            animator.SetBool(FadeOut, true);
            RefreshExpensesInUI();


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

        /// <summary>
        /// Parse the checkboxes and return a dictionary with the relationships and their selected states.
        /// </summary>
        /// <returns>
        /// Dictionary with Family.Relationships as keys and an array of booleans as representing food and meds selected.
        /// </returns>
        public Dictionary<Family.Relationships, bool[]> ParseCheckBoxes()
        {
            var selectedOptions = new Dictionary<Family.Relationships, bool[]>();

            foreach (var familyMember in FamilyCheckBoxes)
            {
                var relationship = familyMember.relationship;
                var medsSelected = familyMember.medsSelected.isOn;
                var foodSelected = familyMember.foodSelected.isOn;
                selectedOptions[relationship] = new[] { foodSelected, medsSelected };
            }

            return selectedOptions;
        }

        public void ChangeFamilyStates()
        {
            foreach (var familyMember in FamilyCheckBoxes)
            {
                var relationship = familyMember.relationship;
                var medsSelected = familyMember.medsSelected.isOn;
                var foodSelected = familyMember.foodSelected.isOn;

                if (GameState.Instance.GetFamilyMemberState(relationship) == Family.States.Sick)
                {
                    if (!medsSelected)
                    {
                        GameState.Instance.SetFamilyMemberState(relationship, Family.States.Dead);
                    }
                    else
                    {
                        GameState.Instance.SetFamilyMemberState(relationship, Family.States.Healthy);
                    }
                }
                else if (GameState.Instance.GetFamilyMemberState(relationship) == Family.States.Hungry)
                {
                    if (foodSelected)
                    {
                        GameState.Instance.SetFamilyMemberState(relationship, Family.States.Healthy);
                    }
                    else
                    {
                        GameState.Instance.SetFamilyMemberState(relationship, Family.States.Dead);
                    }
                }
                else if (GameState.Instance.GetFamilyMemberState(relationship) == Family.States.Healthy)
                {
                    if (!foodSelected)
                    {
                        GameState.Instance.SetFamilyMemberState(relationship, Family.States.Hungry);
                    }
                }
            }
        }

        public int GetTotalExpenses()
        {
            int total = 0;
            foreach (var VARIABLE in FamilyCheckBoxes)
            {
                var medsSelected = VARIABLE.medsSelected.isOn;
                var foodSelected = VARIABLE.foodSelected.isOn;

                if (medsSelected)
                {
                    total += 150;
                }

                if (foodSelected)
                {
                    total += 100;
                }
            }

            return total;
        }

        public void RefreshExpensesInUI()
        {
            foreach (var VARIABLE in FamilyCheckBoxes)
            {
                var relationship = VARIABLE.relationship;
                var medsSelected = VARIABLE.medsSelected.isOn;
                var foodSelected = VARIABLE.foodSelected.isOn;
                int cost = 0;
                var state = GameState.Instance.GetFamilyMemberState(relationship);

                VARIABLE.medsSelected.gameObject.SetActive(
                    state == Family.States.Sick);
                if (GameState.Instance.GetFamilyMemberState(relationship) == Family.States.Dead)
                {
                    VARIABLE.medsSelected.gameObject.SetActive(false);
                    VARIABLE.foodSelected.gameObject.SetActive(false);
                }

                foreach (var a in FamilyCheckBoxes)
                {
                    if (!a.foodSelected.isOn && (GameState.Instance.coinsAccumulated - GetTotalExpenses()) < 100)
                    {
                        a.foodSelected.interactable = false;
                    }
                    else
                    {
                        a.foodSelected.interactable = true;
                    }

                    if (!a.medsSelected.isOn && (GameState.Instance.coinsAccumulated - GetTotalExpenses()) < 150)
                    {
                        a.medsSelected.interactable = false;
                    }

                    else
                    {
                        a.medsSelected.interactable = true;
                    }
                }

                if (medsSelected)
                {
                    cost += 150;
                }

                if (foodSelected)
                {
                    cost += 100;
                }

                VARIABLE.amounTextMeshProUGUI.text = cost.ToString();
            }

            GameState.Instance.UpdateEODText(GameState.Instance.currentEODMessage);
        }

        /// <summary>
        /// Resets all the toggles to off to avoid impossible states, to be used on loading the EOD screen
        /// </summary>
        private void ResetTogglesToFalse()
        {
            foreach (var member in FamilyCheckBoxes)
            {
                member.foodSelected.isOn = false;
                member.medsSelected.isOn = false;
            }
        }
    }
}