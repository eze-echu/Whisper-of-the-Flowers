using System;
using System.Collections.Generic;
using Flowers;
using Racimo;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public string currentEODMessage;

        public static GameState Instance;
        private static bool _isGamePaused;
        public float secondsPerGameDay;
        public int coinsAccumulated;
        public int currentDay = 1;
        private float _timeLeft;
        public float coinMultiplier = 1.00f;
        public float timeMultiplier = 1.00f;

        public OrderSystem OrderSystem;

        public TMP_Text timeText;
        public TMP_Text requestText;

        public Family family;

        // Start is called before the first frame update
        public event Action OnDayChanged;

        [FormerlySerializedAs("_nextDayButton")] [SerializeField]
        private Button nextDayButton;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            secondsPerGameDay = secondsPerGameDay * timeMultiplier;
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

            family = new Family();
            family.LoadFamily();
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
                        _timeLeft = secondsPerGameDay * timeMultiplier;

                        string events = "";
                        (currentEODMessage, coinsAccumulated) = RandomEndOfDayEvent(coinsAccumulated);
                        coinsAccumulated -= 100;
                        family.SickenFamilyMembersRandomly();
                        StartCoroutine(GameManager.instance.EODFS.StartFadeIn(UpdateEODText(currentEODMessage)));
                        Bouquet.Instance.ResetToOriginalState();
                        // StartCoroutine(
                        //     GameManager.instance.Fc.FadeInAndOutCoroutine(
                        //         $"Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: {coinsAccumulated}"));}
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
            coinsAccumulated -= EOD.Instance.GetTotalExpenses();
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

        public string UpdateEODText(string event_text)
        {
            var endOfDayMessage = "Fue un buen dia, pero ya es hora de cerrar la tienda\nMoney: " +
                                  coinsAccumulated;
            // endOfDayMessage += "\n\n" +
            //                    EndOfDayMessage[UnityEngine.Random.Range(0, EndOfDayMessage.Length)];
            endOfDayMessage += event_text;
            foreach (var variable in family.GetFamilyMembers())
            {
                // TODO: Remove this and replace with a check box
                // var cost = variable.Value switch
                // {
                //     Family.States.Dead => 0,
                //     Family.States.Healthy => 100,
                //     Family.States.Sick => 200,
                //     Family.States.Hungry => 150,
                //     Family.States.Cold => 150,
                //     _ => 0
                // };

                // TODO: add check box logic on feeding family and giving them medicine
                // TODO: add hunger on not feeding them
                // coinsAccumulated -= cost;
                // endOfDayMessage += "\n" + variable.Key + ": " + variable.Value + " (-" + cost + ")";
            }

            var a = coinsAccumulated - EOD.Instance.GetTotalExpenses();
            endOfDayMessage += "\n\n" +
                               "-100 coins to pay the bills\n" +
                               "Family Expenses: " + EOD.Instance.GetTotalExpenses() + "\n" +
                               "Coins after tax: " + a;


            if (coinsAccumulated == 0)
            {
                // TODO: Add failure check
            }
            GameManager.instance.EODFS.ChangeEODText(endOfDayMessage);
            return endOfDayMessage;
        }

        private (string, int) RandomEndOfDayEvent(int money)
        {
            var i = UnityEngine.Random.Range(0, EndOfDayMessage.Length);
            var text = "\n\n" + EndOfDayMessage[i];
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

        public static int GetFamilyStateCost(Family.States state)
        {
            return state switch
            {
                Family.States.Healthy => 100,
                Family.States.Sick => 200,
                Family.States.Hungry => 150,
                Family.States.Cold => 150,
                _ => 0
            };
        }

        public void UpdateFamilyExpenses()
        {
            
        }

        public Family.States GetFamilyMemberState(Family.Relationships member)
        {
            return family.GetFamilyMemberState(member);
        }
    }
}