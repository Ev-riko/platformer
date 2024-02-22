
using PixelCrew.Components.Model;
using PixelCrew.Components;
using PixelCrew.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using System;
using Cooldown = PixelCrew.Utils.Cooldown;
using Assets.PIxelCrew.Model;
using System.Collections;
using PIxelCrew;

namespace PixelCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private float _slamDownVelocity;

        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private ProbabilityDropComponent _hitDrop;

        [Space]
        [Header("Animation")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _desarmed;


        [Space]
        [Header("Super throw")]
        [SerializeField] private Cooldown _superThrowCooldawn;
        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;

        

        private int CoinsCount => _session.Data.inventory.Count("Coin");
        private int SwordCount => _session.Data.inventory.Count("Sword");
        private int PotionHealthCount => _session.Data.inventory.Count("PotionHealth");


        private bool _allowDoubleJump;
        private bool _isOnWall;
        private bool _superThrow;

        private float _defaultGravityScale;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private GameSession _session;
        private HealthComponent _health;
        
        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = _rigitbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _health = GetComponent<HealthComponent>();
            _session.Data.inventory.OnChanged += OnInventoryChanged;

            _health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.inventory.OnChanged -= OnInventoryChanged;
        }




        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = _direction.x * transform.localScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {

                _isOnWall = true;
                _rigitbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                _rigitbody.gravityScale = _defaultGravityScale;
            }

            _animator.SetBool(IsOnWall, _isOnWall);
        }



        protected override float CalculateYVelosity()
        {
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0;
            }

            return base.CalculateYVelosity();
        }

        protected override float CalculateJumpVelosity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump && !_isOnWall)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }
            return base.CalculateJumpVelosity(yVelocity);
        }
        public void SaySomething()
        {
            Debug.Log("Something");
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.inventory.Add(id, value);
        }


        public void PlayPotionEffect()
        {
            _particles.Spawn("Potion");
        }



        public override void TakeDamage()
        {
            base.TakeDamage();

            //Debug.Log($"_coins: {CoinsCount}");
            if (CoinsCount > 0)
                SpawnCoins();
        }

        private void SpawnCoins()
        {
            Debug.Log("SpawnCoins");
            var numCoinsDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.inventory.Remove("Coin", numCoinsDispose);


            _hitDrop.SetCount(numCoinsDispose);
            _hitDrop.CalculateDrop();
            
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(_groundLayer))
            {
                var contact = collision.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
            }

        }


        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
        }

        public void ArmHero()
        {
            _session.Data.inventory.Add("Sword", 1);
            UpdateHeroWeapon();
        }

        public void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _desarmed;
        }

        public void OnDoThrow()
        {
            if (_superThrow) 
            {
                var numThrows = Mathf.Min(_superThrowParticles, SwordCount - 1);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()
        {
            _particles.Spawn("Throw");
            _session.Data.inventory.Remove("Sword", 1);
        }

        public void Throw()
        {
            if (_throwCooldown.IsReady && SwordCount > 1)
            {
                Debug.Log("Throw");
                _animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
            }
        }

        public void Heal()
        {
            if (PotionHealthCount > 0)
            {
                _session.Data.inventory.Remove("PotionHealth", 1);
                _health.ModifyHealth(5);
                PlayPotionEffect();
            }
        }

        public void StartThrowing()
        {
            _superThrowCooldawn.Reset();
        }

        public void PerformThrowing()
        {
            if (!_throwCooldown.IsReady || SwordCount <= 1) return;

            if (_superThrowCooldawn.IsReady)
            {
                _superThrow = true;
            }

            _animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }
    }
}
