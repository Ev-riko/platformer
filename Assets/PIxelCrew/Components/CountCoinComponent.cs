using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PixelCrew.Components
{
    public class CountCoinComponent : MonoBehaviour
    {
        private Hero _hero;
        private Collider2D _collider;

        private void Awake()
        {
            _hero = FindObjectOfType<Hero>();
            _collider = GetComponent<Collider2D>();
        }

        public void AddCoin(int count)
        {
            _hero.AddCoins(count);
            _collider.enabled = false;
        }
    }
}