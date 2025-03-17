using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

   [CreateAssetMenu(fileName = "CoinMultiplier", menuName = "Store Effects/Multiple Coins")]
    public class CoinMultiplier : StoreEffect
    {
        public float amount = 2f;

        public override void Apply()
        {
            GameState.Instance.coinMultiplier += amount;
            Debug.Log($"Se aplico multiplicador de monedas");
        }
    }
