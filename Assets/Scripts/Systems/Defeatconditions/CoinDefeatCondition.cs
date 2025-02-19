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

            DefeatConditions.AddConditions(this);
        }

        public bool CheckCondition()
        {
            // cada una semana verifica las cant de monedas
            if (_gameState.currentDay % 7 == 0 && _gameState.coinsAccumulated < _requiredCoins)
            {
                DefeatConditions.Instance.CheckDefeat(_infoDefeat);
                return true;
            }
            return false;
        }
    }

}
