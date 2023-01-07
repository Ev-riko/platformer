using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PixelCrew.Component
{
    public class CountCoinComponent : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void AddSilverCoin()
        {
            _hero.AddCoin(1);
        }

        public void AddGoldCoin()
        {
            _hero.AddCoin(10);
        }
    }
}