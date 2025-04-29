using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

   [CreateAssetMenu(fileName = "CoinMultiplier", menuName = "Store Effects/Multiple Coins")]
    public class CoinMultiplier : StoreEffect
    {
        public enum TypeMultiplier
        {
            Money,
            Time

        }

        public TypeMultiplier type;
        public float amount = 2f;



        public override void Apply()
        {

            switch (type)
            {
                case TypeMultiplier.Money:
                    GameState.Instance.coinMultiplier += amount;
                    Debug.Log("Se aplicó multiplicador de monedas");
                    break;

                case TypeMultiplier.Time:
                    GameState.Instance.timeMultiplier += amount;
                    Debug.Log("Se aplicó multiplicador de tiempo");
                    break;

                default:
                    Debug.LogWarning("Tipo de multiplicador no reconocido");
                    break;
            }
        }
    }
