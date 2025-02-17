using System;
using Flowers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance;
        private static bool _isGamePaused;
        public static int secondsPerGameDay = 120;
        public uint coinsAccumulated;
        public int currentDay = 1;
        public float timeLeft = secondsPerGameDay;
        public float coinMultiplier = 1.00f;

        public OrderSystem OrderSystem;

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
                if (timeLeft > 0) timeLeft -= Time.deltaTime;
                timeText.text =
                    $"Day {currentDay} - {(uint)Mathf.Floor(timeLeft / 60)}:{(uint)Mathf.Floor(timeLeft % 60):D2}";
                if (timeLeft <= 0.5f) // Es .5 pq si no se pasa de largo y va a negativo (o underflow a max int value)
                {
                    timeLeft = secondsPerGameDay;
                    StartCoroutine(
                        GameManager.instance.Fc.FadeInAndOutCoroutine(
                            $"Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: {coinsAccumulated}"));
                    // TODO: Implement end of day logic (1st calculate earnings, save it, then reset the day, if 7th and below required, fail the game)
                    NewRequest();
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
            OrderSystem = new OrderSystem();
            OrderSystem.GenerateOrder();
            // Values are added in the FlowerHandler.cs
            requestText.text =
                $"{OrderSystem.get_order_message(0)}\n{OrderSystem.get_order_message(1)}\n{OrderSystem.get_order_message(2)}";
        }

        public void AddRequestReward(float grade)
        {
            coinsAccumulated += (uint)(OrderSystem.GetOrderReward() * grade * coinMultiplier);
        }
    }
}