using System;
using System.Collections.Generic;
using Flowers;
using Racimo;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems
{
    public class GameState : MonoBehaviour
    {
        private readonly string[] EndOfDayMessage = new[]
        {
            "Tuviste un accidente -500 monedas",
            "Tuviste MUCHA suerte +500 monedas",
            "Tenias antojo de algo dulce -200 monedas",
        };
        public static GameState Instance;
        private static bool _isGamePaused;
        public float secondsPerGameDay;
        public int coinsAccumulated;
        public int currentDay = 1;
        private float _timeLeft;
        public float coinMultiplier = 1.00f;

        public OrderSystem OrderSystem;

        public TMP_Text timeText;
        public TMP_Text requestText;

        // Start is called before the first frame update
        public event Action OnDayChanged;
        [FormerlySerializedAs("_nextDayButton")] [SerializeField] private Button nextDayButton;

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
            if (saveData.ContainsKey("coins") && int.TryParse(saveData["coins"].ToString(), out var coins))
            {
                coinsAccumulated = coins;
            }

            if (saveData.ContainsKey("daysPlayed") && int.TryParse(saveData["daysPlayed"].ToString(), out var day))
            {
                currentDay = day;
            }
            _isGamePaused = false;
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
                    if (currentDay == 3)
                    {
                        var endOfDayMessage = "Fue una buena semana, esperamos que la hayas disfrutado";
                        nextDayButton.GetComponentInChildren<TextMeshProUGUI>().text = "Main Menu";
                        nextDayButton.onClick.RemoveAllListeners();
                        nextDayButton.onClick.AddListener(() => SceneLoader.Instance().AsyncLoadScene("MainMenu"));
                        nextDayButton.onClick.AddListener(Save.DeleteData);
                        StartCoroutine(GameManager.instance.EODFS.StartFadeIn(endOfDayMessage));
                    }
                    else
                    {
                        _timeLeft = secondsPerGameDay;
                        // StartCoroutine(
                        //     GameManager.instance.Fc.FadeInAndOutCoroutine(
                        //         $"Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: {coinsAccumulated}"));}
                        var endOfDayMessage = "Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: " +
                                              coinsAccumulated;
                        // endOfDayMessage += "\n\n" +
                        //                    EndOfDayMessage[UnityEngine.Random.Range(0, EndOfDayMessage.Length)];
                        (endOfDayMessage, coinsAccumulated) = RandomEndOfDayEvent(endOfDayMessage, coinsAccumulated);
                        coinsAccumulated -= 200;
                        endOfDayMessage += "\n\n" +
                                           "-100 coins to pay the bills\n" +
                                           "Coins after tax: " + coinsAccumulated;

                        if (coinsAccumulated == 0)
                        {
                            // TODO: Add failure check
                        }

                        StartCoroutine(GameManager.instance.EODFS.StartFadeIn(endOfDayMessage));
                        Bouquet.Instance.ResetToOriginalState();
                    }
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
            StartCoroutine(GameManager.instance.EODFS.StartFadeOut());
            CameraController.instance.SwitchToSpecificCamera(Bouquet.Workstations.VaseStation);
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

        private (string, int) RandomEndOfDayEvent(string text, int money)
        {
            var i = UnityEngine.Random.Range(0, EndOfDayMessage.Length);
            text += "\n\n" + EndOfDayMessage[i];
            switch (i)
            {
                case 0:
                    money -= 500;
                    break;
                case 1:
                    money += 500;
                    break;
                case 2:
                    money -= 200;
                    break;

                default:
                    money = money;
                    break;
            }
            return (text, money);
        }

        public void AddRequestReward(float grade)
        {
            coinsAccumulated += (int)(OrderSystem.GetOrderReward() * grade * coinMultiplier);
        }
    }
}