using UnityEngine;

namespace PixelCrew.Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _power;

        public void ModifyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_power);
            }
        }
    }
}