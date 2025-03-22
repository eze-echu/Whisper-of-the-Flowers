using System;
using System.Collections.Generic;
using Flowers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class GameState : MonoBehaviour
    {
        private readonly string[] EndOfDayMessage = new[]
        {
            "Mondongo",
            "Mandinga",
            "Sorondongo",
        };
        public static GameState Instance;
        private static bool _isGamePaused;
        public float secondsPerGameDay;
        public uint coinsAccumulated;
        public int currentDay = 1;
        private float _timeLeft;
        public float coinMultiplier = 1.00f;

        public OrderSystem OrderSystem;

        public TMP_Text timeText;
        public TMP_Text requestText;

        // Start is called before the first frame update
        public event Action OnDayChanged;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            _timeLeft = secondsPerGameDay;
        }

        void Start()
        {
            NewRequest();
            var saveData = Save.LoadDirectly();
            if (saveData.ContainsKey("coins") && uint.TryParse(saveData["coins"].ToString(), out var coins))
            {
                coinsAccumulated = coins;
            }

            if (saveData.ContainsKey("daysPlayed") && int.TryParse(saveData["daysPlayed"].ToString(), out var day))
            {
                currentDay = day;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isGamePaused)
            {
                if (_timeLeft > 0) _timeLeft -= Time.deltaTime;
                timeText.text =
                    $"Day {currentDay} - {(int)Mathf.Floor(_timeLeft / 60)}:{(int)Mathf.Floor(_timeLeft % 60):D2}";
                if (_timeLeft <= 0.5f) // Es .5 pq si no se pasa de largo y va a negativo (o underflow a max int value)
                {
                    _timeLeft = secondsPerGameDay;
                    // StartCoroutine(
                    //     GameManager.instance.Fc.FadeInAndOutCoroutine(
                    //         $"Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: {coinsAccumulated}"));
                    StartCoroutine(GameManager.instance.Fc.StartFadeIn(
                        $"Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: {coinsAccumulated}\n\n{EndOfDayMessage[UnityEngine.Random.Range(0, EndOfDayMessage.Length)]}"));
                    // TODO: Implement end of day logic (1st calculate earnings, save it, then reset the day, if 7th and below required, fail the game)
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
            NewRequest();
            Save.SaveData(
                new Dictionary<string, object> { { "coins", coinsAccumulated }, { "daysPlayed", currentDay } });
            currentDay = day;
            OnDayChanged?.Invoke();
            StartCoroutine(GameManager.instance.Fc.StartFadeOut());
        }

        public void NextDay()
        {
            ChangeDay(currentDay + 1);
        }

        public void NewRequest()
        {
            OrderSystem = new OrderSystem();
            OrderSystem.GenerateRandomOrder();
            // Values are added in the FlowerHandler.cs
            requestText.text =
                $"{OrderSystem.get_order_message(0)}\n{OrderSystem.get_order_message(1)}\n{OrderSystem.get_order_message(2)}\n\n{OrderSystem.GetOrderVase()}";
        }

        public void AddRequestReward(float grade)
        {
            coinsAccumulated += (uint)(OrderSystem.GetOrderReward() * grade * coinMultiplier);
        }
    }
}