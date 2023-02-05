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
            Debug.Log($"DamageValue: {DamageValue}, _currentHealth: {_currentHealth}");
            if (_currentHealth <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void ApllyHeal(int HealValue)
        {           
            _currentHealth = System.Math.Min(_maxHealth, _currentHealth + HealValue);
            Debug.Log($"HealValue: {HealValue}, _currentHealth: {_currentHealth}");
            _onHeal?.Invoke();
        }
    }
}