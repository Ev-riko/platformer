using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] public UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private HealthChangeEvent _onChange;

        private int _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            _onChange?.Invoke(_health);
            if (healthDelta > 0) 
            {   
                _health = System.Math.Min(_maxHealth, _health + healthDelta);
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
                if (_health <= 0)
                {
                    Debug.Log($"_onDie");
                    _onDie?.Invoke();
                }
            }  
        }

#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        public void UpdateHealth()
        {
            _onChange?.Invoke(_health);
        }
#endif

        public void SetHealth(int health)
        {
            _health = health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }
    }
}