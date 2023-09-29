
using PixelCrew.Components.Model;
using PixelCrew.Components;
using PixelCrew.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace PixelCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private float _slamDownVelocity;

        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _desarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;


        private bool _allowDoubleJump;
        private bool _isOnWall;
        private float _defaultGravityScale;


        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = _rigitbody.gravityScale;
        }

        private void OnEnable()
        {
            _hitParticles.GameObject().SetActive(false);
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        public void OnHealthGanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }




        protected override void Update()
        {
            base.Update();
            if (_wallCheck.IsTouchingLayer && _direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                _rigitbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                _rigitbody.gravityScale = _defaultGravityScale;
            }
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
            if (!_isGrounded && _allowDoubleJump)
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

        public void AddCoins(int coins)
        {
            _session.Data.Coins += coins;
            Debug.Log($"Added: {coins} Total: {_session.Data.Coins}");
        }

        public void PlayPotionEffect()
        {
            _particles.Spawn("Potion");
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            Debug.Log($"_coins: {_session.Data.Coins}");
            if (_session.Data.Coins > 0)
                SpawnCoinsParticles();
        }

        private void SpawnCoinsParticles()
        {
            Debug.Log("SpawnCoinsParticles");
            var numCoinsDispose = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
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
            if (!_session.Data.IsArmed) return;

            base.Attack();
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();           
        }

        public void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _desarmed;
        }

    }
}
