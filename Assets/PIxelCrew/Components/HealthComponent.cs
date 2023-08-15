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

        private int _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void ModifyHealth(int healthDelta)
        {
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
                if (_health <= 0)
                {
                    Debug.Log($"_onDie");
                    _onDie?.Invoke();
                }
                else
                {
                    Debug.Log($"_onDamage");
                    _onDamage?.Invoke();
                }
            }  
        }
    }
}