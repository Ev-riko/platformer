using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] public UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        public delegate void OnHealthChanged(int currentHealth, int maxHealth);
        public OnHealthChanged OnChanged;


        private void Start()
        {
            _health = _maxHealth;
        }

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            
            if (healthDelta > 0) 
            {   
                _health = Mathf.Min(_maxHealth, _health + healthDelta);
                
                Debug.Log($"HealValue: {healthDelta}, _health: {_health}");
                Debug.Log($"_onHeal");
               
                _onHeal?.Invoke();
            }
            else if (healthDelta < 0)
            {
                _health += healthDelta;
                
                Debug.Log($"DamageValue: {healthDelta}, _health: {_health}");
                Debug.Log($"_onDamage");

                _onDamage?.Invoke();
                
            }

            OnChanged?.Invoke(_health, _maxHealth);

            if (_health <= 0)
            {
                Debug.Log($"_onDie");

                _onDie?.Invoke();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        public void UpdateHealth()
        {
            OnChanged?.Invoke(_health, _maxHealth);
        }
#endif

        public void SetHealth(int health)
        {
            _health = health;
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }
    }
}