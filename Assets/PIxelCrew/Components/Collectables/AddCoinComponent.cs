using PixelCrew.Creatures;
using UnityEngine;


namespace PixelCrew.Components
{
    public class AddCoinComponent : MonoBehaviour
    {
        private Hero _hero;
        private Collider2D _collider;

        private void Awake()
        {
            Debug.Log("Awake");
            _hero = FindObjectOfType<Hero>();
            _collider = GetComponent<Collider2D>();
        }



        public void AddCoin(int count)
        {
            if (_hero == null) Awake();
            _hero.AddCoins(count);
            _collider.enabled = false;
        }
    }
}