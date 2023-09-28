using PixelCrew.Creatures;
using UnityEngine;

namespace PixelCrew.Components
{
    public class PotionComponent : MonoBehaviour
    {
        public void StartPotionAnimation(GameObject target)
        {
            Debug.Log("StartPotionAnimation");
            var hero = target.GetComponent<Hero>();
            if (hero != null)
            {
                hero.PlayPotionEffectAnimation();
            }
        }
    }
}