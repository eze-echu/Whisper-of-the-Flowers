using System;
using Flowers;
using UnityEngine;

namespace Systems
{
    public class OrderSystem
    {
        private struct Order
        {
            internal FlowerMessageType[] Messages;
            internal string Vase;
            internal uint Reward;
        }

        public FlowerMessageType get_order_message(uint orderNumber)
        {
            return _currentOrder.Messages[orderNumber];
        }

        public OrderSystem()
        {
            GenerateOrder();
        }

        private Order _currentOrder;

        public float
            GradeBouquet(Bouquet bouquet) // returns a float between 0 and 1 representing the grade of the bouquet
        {
            //TODO: add depth to bouquet grading
            return bouquet.GetValues().message == get_order_message(1) ? 1 : 0;
        }

        public void GenerateOrder()
        {
            _currentOrder.Messages = new FlowerMessageType[3];
            for (uint iterator = 0; iterator < _currentOrder.Messages.Length; iterator++)
            {
                var a = (FlowerMessageType)UnityEngine.Random.Range(0, FlowerHandler.GetFlowerMessages().Length - 1);
                while (Array.Exists(_currentOrder.Messages, element => element == a) || a == FlowerMessageType.Null)
                {
                    a = (FlowerMessageType)UnityEngine.Random.Range(1, FlowerHandler.GetFlowerMessages().Length);
                }

                _currentOrder.Messages[iterator] = a;
            }

            _currentOrder.Vase = "Vase";
            _currentOrder.Reward = 100;
        }

        public float GetOrderReward()
        {
            return _currentOrder.Reward;
        }
    }
}