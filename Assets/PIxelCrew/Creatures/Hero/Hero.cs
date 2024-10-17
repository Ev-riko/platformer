
using PixelCrew.Components;
using PixelCrew.Utils;
using UnityEngine;
using Cooldown = PixelCrew.Utils.Cooldown;
using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Model;
using PixelCrew.Components.Health;
using PixelCrew.Model.Definitions;
using Unity.VisualScripting;
using System;
using PixelCrew.Components.GoBased;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private float _slamDownVelocity;

        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private SpawnComponent _throwSpawner;
        [SerializeField] private ProbabilityDropComponent _hitDrop;


        [Space]
        [Header("Animation")]
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _desarmed;


        [Space]
        [Header("Super throw")]
        [SerializeField] private Cooldown _superThrowCooldawn;
        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;

        private bool _allowDoubleJump;
        private bool _isOnWall;
        private bool _superThrow;

        private float _defaultGravityScale;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private GameSession _session;
        private HealthComponent _health;

        private const string SwordId = "Sword";
        private int CoinsCount => _session.Data.inventory.Count("Coin");
        private int SwordCount => _session.Data.inventory.Count(SwordId);
        private int PotionHealthCount => _session.Data.inventory.Count("PotionHealth");

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigitbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.inventory.OnChanged += OnInventoryChanged;

            _health = GetComponent<HealthComponent>();
            _health.SetMaxHealth(DefsFacade.I.Player.MaxHealth);
            _health.SetHealth(_session.Data.Hp.Value);
            _health.OnChanged += OnHealthChanged;

            UpdateHeroWeapon();
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.inventory.OnChanged -= OnInventoryChanged;
            _health.OnChanged -= OnHealthChanged;
        }

        public void OnHealthChanged(int currentHealth, int maxHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }


        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.localScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {

                _isOnWall = true;
                Rigitbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigitbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsOnWall, _isOnWall);
        }



        protected override float CalculateYVelosity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
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
            if (!IsGrounded && _allowDoubleJump && !_isOnWall)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
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
            Sounds.PlayClip("Melee");
        }

        public void ArmHero()
        {
            _session.Data.inventory.Add(SwordId, 1);
            UpdateHeroWeapon();
        }

        public void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _desarmed;
        }

        public void OnDoThrow()
        {
            if (_superThrow)
            {
                _superThrow = false;
                var throwableCount = _session.Data.inventory.Count(SelectedItemId);
                var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
                var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
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
            Sounds.PlayClip("Range");
            //_particles.Spawn("Throw");
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDef = DefsFacade.I.ThrowableItems.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();

            _session.Data.inventory.Remove(throwableId, 1);
        }

        public void Heal()
        {
            var HealId = _session.QuickInventory.SelectedItem.Id;
            var HealDef = DefsFacade.I.HealingItems.Get(HealId);

            _health.ModifyHealth(HealDef.Power);
            PlayPotionEffect();

            _session.Data.inventory.Remove(HealId, 1);
        }

        public void PlayPotionEffect()
        {
            _particles.Spawn("Potion");
        }

        public void StartThrowing()
        {
            _superThrowCooldawn.Reset();
        }

        public void PerformThrowing()
        {
            if (!_throwCooldown.IsReady || !CanThrow) return;

            if (_superThrowCooldawn.IsReady)
            {
                _superThrow = true;
            }

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }
    }
}
