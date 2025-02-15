using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class GameState : MonoBehaviour
    {
        private readonly string[] _a = { "Love", "Decrease_of_Love", "Jealousy", "Mourning", "Hatred" };

        public static GameState Instance;
        private static bool _isGamePaused;
        [SerializeField] public static int secondsPerGameDay = 120;
        public int coinsAccumulated = 0;
        public int currentDay = 1;
        public float timeLeft = secondsPerGameDay;
        public float coinMultiplier = 1.00f;

        public TMP_Text timeText;
        public TMP_Text requestText;

        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            NewRequest();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isGamePaused)
            {
                timeLeft -= Time.deltaTime;
                timeText.text = "Day " + currentDay + " - " + Mathf.Floor(timeLeft / 60) + ":" +
                                Mathf.Floor(timeLeft % 60);
                if (timeLeft <= 0)
                {
                    timeLeft = secondsPerGameDay;
                    StartCoroutine(
                        GameManager.instance.Fc.FadeInAndOutCoroutine(
                            "Fue un buen dia, pero ya es hora de cerrar la tienda"));
                    // TODO: Implement end of day logic (1st calculate earnings, save it, then reset the day, if 7th and below required, fail the game)
                    currentDay++;
                }
            }
        }

        public static void PauseGame()
        {
            _isGamePaused = true;
        }

        public static void ResumeGame()
        {
            _isGamePaused = false;
            Time.timeScale = 1;
        }

        public void ChangeDay(int day)
        {
            //TODO: Add logic for earning calculation and saving
        }

        public void NewRequest()
        {
            requestText.text =
                $"{_a[UnityEngine.Random.Range(0, _a.Length)]}\n{_a[UnityEngine.Random.Range(0, _a.Length)]}\n{_a[UnityEngine.Random.Range(0, _a.Length)]}";
        }
    }
}