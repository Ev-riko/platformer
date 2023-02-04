using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DamageHealComponent : MonoBehaviour
    {
        [SerializeField] private int _power;

        public void ApllyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApllyDamage(_power);
            }
        }

        public void ApllyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApllyHeal(_power);
            }
        }
    }
}