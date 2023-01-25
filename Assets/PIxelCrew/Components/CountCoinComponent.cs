using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PixelCrew.Components
{
    public class CountCoinComponent : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void AddSilverCoin(int count)
        {
            _hero.AddCoins(1);
        }

        public void AddGoldCoin()
        {
            _hero.AddCoins(10);
        }
    }
}