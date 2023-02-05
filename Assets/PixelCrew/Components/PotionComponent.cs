using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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