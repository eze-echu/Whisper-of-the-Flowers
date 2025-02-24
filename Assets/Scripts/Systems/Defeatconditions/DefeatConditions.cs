using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using TMPro;

public class DefeatConditions : MonoBehaviour
{
        public bool AlreadyLoose { get; private set; } = false;
         public static DefeatConditions Instance { get; private set; }
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text defeatReasonText;

        private static readonly List<IDefeatCondition> _defeatConditions = new List<IDefeatCondition>();
        public static IReadOnlyList<IDefeatCondition> Conditions => _defeatConditions.AsReadOnly();

        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !AlreadyLoose)
            {
                AlreadyLoose = true;
                CheckDefeat("Menem");
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                GameState.Instance.coinsAccumulated = 0;
                Debug.Log($"Monedas reducidas. Ahora tienes: {GameState.Instance.coinsAccumulated}");
            }
        }
        #endif

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

    public void Start()
    {
        AddConditions(new CoinDefeatCondition(GameState.Instance, 1));
    }

    public static void AddConditions(IDefeatCondition condition)
    {
        _defeatConditions.Add(condition);
    }  
        public void CheckDefeat(string info)
        {
            if (AlreadyLoose)
            {
                if (gameOverPanel != null)
                {
                    gameOverPanel.SetActive(true);
                    Vector3 targetPos = new Vector3(gameOverPanel.transform.position.x, 100f, gameOverPanel.transform.position.z);
                    StartCoroutine(AnimatePanelToTarget(gameOverPanel.transform, targetPos, 1f));
                    defeatReasonText.text = info;
                    //Time.timeScale = 0;
                }
                else
                {
                    Debug.LogWarning("El panel de Game Over no está asignado en DefeatConditions.");
                }
            }
        }

        private IEnumerator AnimatePanelToTarget(Transform panelTransform, Vector3 targetPos, float duration)
        {
            Vector3 startPos = panelTransform.position;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                panelTransform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
                yield return null;
            }
            panelTransform.position = targetPos;
        }
}
