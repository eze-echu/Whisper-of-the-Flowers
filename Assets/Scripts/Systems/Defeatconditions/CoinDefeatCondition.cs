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
        private readonly string _infoDefeat;

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
            
            Debug.Log("Se cambio el dia");
            if (_currentDay % 7 == 0 && _coinsAccumulated < _requiredCoins)
            {
                DefeatConditions.Instance.CheckDefeat(_infoDefeat);
            }
        }
        public bool CheckCondition()
        {
         
            return false;
        }
    }

}
