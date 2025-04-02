using System.Collections;
using System.Collections.Generic;
using Systems;
using Unity.VisualScripting;
using UnityEngine;

namespace System
{


    public class CoinDefeatCondition : IDefeatCondition
    {
        private readonly GameState _gameState;
        private readonly uint _requiredCoins;
        private readonly string _infoDefeat = "Congrats, you're poor";

        public CoinDefeatCondition(GameState gameState, uint requiredCoins)
        {
            _gameState = gameState;
            _requiredCoins = requiredCoins;
            //_infoDefeat = infoDefeat;
            _gameState.OnDayChanged += CheckOnDayChange;
        }

        private void CheckOnDayChange()
        {
            var _currentDay = _gameState.currentDay;
            var _coinsAccumulated = _gameState.coinsAccumulated;
            
            //Debug.Log("SE CAMBIO DE DIA");
            if (_coinsAccumulated <= _requiredCoins && _currentDay % 7 == 0 && _currentDay != 0)
            {
                Debug.Log("Entro en el if de CheckDefeat de CoinCondition");
                DefeatConditions.Instance.BoolDefeat(true);
                DefeatConditions.Instance.CheckDefeat(_infoDefeat);
            }
        }
        public bool CheckCondition()
        {         
            return false;
        }
    }

}
