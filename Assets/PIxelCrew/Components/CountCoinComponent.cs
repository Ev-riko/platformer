using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PixelCrew.Components
{
    public class CountCoinComponent : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void AddCoin(int count)
        {
            _hero.AddCoins(count);
            _collider.enabled = false;
        }
    }
}