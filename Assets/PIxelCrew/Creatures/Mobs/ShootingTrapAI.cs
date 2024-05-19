using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Utils;
using System;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    [Serializable]
    public class ShootingTrapAI : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] public ColliderCheck _vision;

        protected Animator _animator;

        private static readonly int Range = Animator.StringToHash("range");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            if (_vision.IsTouchingLayer && _rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }

        public void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}