using System;
using System.Linq;
using Flowers;
using Racimo;
using Racimo.Vase;
using UnityEngine;
using Bouquet = Racimo.Bouquet;
using Random = UnityEngine.Random;

namespace Systems
{
    public class OrderSystem
    {
        private struct Order
        {
            internal FlowerMessageType[] Messages;
            internal VaseType Vase;
            internal uint Reward;
        }

        public FlowerMessageType get_order_message(uint orderNumber)
        {
            return _currentOrder.Messages[orderNumber];
        }

        public OrderSystem()
        {
            GenerateRandomOrder();
        }

        private Order _currentOrder;

        public float
            CompleteOrder(Bouquet bouquet) // returns a float between 0 and 1 representing the grade of the bouquet
        {
            float grade = 0;
            float extraGrade = 0;
            //TODO: add depth to bouquet grading
            for(int i = 0; i < _currentOrder.Messages.Length; i++)
            {
                if (bouquet.GetMessages().Contains(_currentOrder.Messages[i]))
                {
                    grade += 0.5f;
                    if (bouquet.GetMessages()[i] == _currentOrder.Messages[i])
                    {
                        grade += 0.25f;
                    }
                }

            }
            if (bouquet.GetVaseType() == _currentOrder.Vase)
            {
                extraGrade += 0.25f;
            }
            return grade / _currentOrder.Messages.Length + extraGrade;
        }

        public void GenerateRandomOrder()
        {
            //Debug.Log(String.Join(",\n", FlowerHandler.instance.GetFlowerMessages()));
            _currentOrder.Messages = new FlowerMessageType[3];
            for (uint iterator = 0; iterator < _currentOrder.Messages.Length; iterator++)
            {
                var a = FlowerHandler.instance.GetFlowerMessages()[Random.Range(0, FlowerHandler.instance.GetFlowerMessages().Length)];
                // Make Unique flowers on the request
                // while (Array.Exists(_currentOrder.Messages, element => element == a) || a == FlowerMessageType.Null)
                // {
                //     a = FlowerHandler.instance.GetFlowerMessages()[Random.Range(0, FlowerHandler.instance.GetFlowerMessages().Length)];
                // }

                _currentOrder.Messages[iterator] = a;
            }
            //Debug.Log(String.Join(";\n", _currentOrder.Messages));

            _currentOrder.Vase = VaseHandler.Instance.GetAvailableVaseTypes()[Random.Range(0, VaseHandler.Instance.GetAvailableVaseTypes().Length)];
            _currentOrder.Reward = 100;
        }

        public float GetOrderReward()
        {
            return _currentOrder.Reward;
        }

        public string GetOrderVase()
        {
            return _currentOrder.Vase.ToString();
        }
    }
}