using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    public class PotionComponent : MonoBehaviour
    {
        public void StartPotionAnimation(GameObject target)
        {
            Debug.Log("StartPotionAnimation");
            var hero = target.GetComponent<Hero>();
            if (hero != null)
            {
                hero.PlayPotionEffect();
            }
        }
    }
}