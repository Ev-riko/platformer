
using PixelCrew.Components;
using PixelCrew.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpd;
        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private int _damage;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] private GameObject _AttackEffect;
        [SerializeField] private GameObject _PotionEffect;
        [SerializeField] private CheckCircleOverlap _attackRange;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _desarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private SpawnComponent _footStepParticles;
        [SerializeField] private SpawnComponent _jumpParticles;
        [SerializeField] private SpawnComponent _SlamDownParticles;
        [SerializeField] private ParticleSystem _hitParticles;

        private Collider2D[] _interactionResult = new Collider2D[1];
        private Vector2 _direction;
        private Rigidbody2D _rigitbody;
        private Animator _animator;
        private SpriteRenderer _sprite;
        private bool _isGrounded;
        private bool _allowDoubleJump;
        private bool _isJumping;


        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        private int _coins;

        private bool _isArmed = false;

        private void Awake()
        {
            _rigitbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();

        }

        private void OnEnable()
        {
            _hitParticles.GameObject().SetActive(false);

        }

        private void Start()
        {
            _PotionEffect.SetActive(false);
            _coins = 0;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }



        private void Update()
        {
            _isGrounded = IsGrounded();
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelosity();
            _rigitbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetFloat(VerticalVelocityKey, _rigitbody.velocity.y);
            _animator.SetBool(IsRunningKey, _direction.x != 0);
            _animator.SetBool(IsGroundKey, _isGrounded);

            UpdateSpriteDirection();
        }

        private float CalculateYVelosity()
        {
            var yVelocity = _rigitbody.velocity.y;
            var isLumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDoubleJump = true;
                _isJumping = false;
            }

            if (isLumpPressing)
            {
                _isJumping = true;
                yVelocity = CalculateJumpVelosity(yVelocity);
            }
            else if (_rigitbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        private float CalculateJumpVelosity(float yVelocity)
        {
            var isFalling = _rigitbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;
            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                Debug.Log("Jump");
                SpawnJumpDust();
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                _allowDoubleJump = false;
                Debug.Log("DoubleJump");
                SpawnJumpDust();
            }
            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private bool IsGrounded()
        {
            var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius,
                Vector2.down, 0, _groundLayer);
            return hit.collider != null;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
#endif
        public void SaySomething()
        {
            Debug.Log("Something");
        }

        public void AddCoins(int coins)
        {
            _coins += coins;
            Debug.Log($"Added: {coins} Total: {_coins}");
        }

        public void PlayPotionEffectAnimation()
        {
            _PotionEffect.SetActive(true);
        }

        public void TakeDamage()
        {
            Debug.Log("TakeDamage");
            _isJumping = false;
            _animator.SetTrigger(Hit);
            _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, _damageJumpSpd);
            Debug.Log($"_coins: {_coins}");
            if (_coins > 0)
                SpawnCoinsParticles();
        }

        private void SpawnCoinsParticles()
        {
            Debug.Log("SpawnCoinsParticles");
            var numCoinsDispose = Mathf.Min(_coins, 5);
            _coins -= numCoinsDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer);
            Debug.Log($"Interact, size: {size}");

            for (int i = 0; i < size; i++)
            {
                var interactble = _interactionResult[i].GetComponent<InteractableComponent>();
                if (interactble != null)
                {
                    interactble.Interact();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(_groundLayer))
            {
                var contact = collision.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    SpawnSlamDownDust();
                }
            }

        }

        public void SpawnFootStepDust()
        {
            _footStepParticles.Spawn();
        }

        public void SpawnJumpDust()
        {
            _jumpParticles.Spawn();
        }

        public void SpawnSlamDownDust()
        {
            _SlamDownParticles.Spawn();
        }

        public void Attack()
        {
            if (!_isArmed) return;

            _animator.SetTrigger(AttackKey);
        }

        public void MakeAttack()
        {
            PlayAttackEffectAnimation();
            Debug.Log("OnAttack");
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))
                {
                    hp.ModifyHealth(-_damage);
                }
            }
        }

        public void ArmHero()
        {
            _animator.runtimeAnimatorController = _armed;
            _isArmed = true;
        }

        public void PlayAttackEffectAnimation()
        {
            _AttackEffect.SetActive(true);
        }
    }
}
