using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void ApllyDamage(int DamageValue)
        {
            _currentHealth -= DamageValue;
            _onDamage?.Invoke();
            if (_currentHealth <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void ApllyHeal(int HealValue)
        {
            _currentHealth = System.Math.Max(_maxHealth, _currentHealth + HealValue);
            _onHeal?.Invoke();
        }
    }
}