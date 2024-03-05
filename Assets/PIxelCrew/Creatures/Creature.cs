using PixelCrew.Components;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageJumpSpd;
        [SerializeField] private int _damage;

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;
        
        protected Vector2 Direction;
        protected Rigidbody2D Rigitbody;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigitbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelosity();
            Rigitbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetFloat(VerticalVelocityKey, Rigitbody.velocity.y);
            Animator.SetBool(IsRunningKey, Direction.x != 0);
            Animator.SetBool(IsGroundKey, IsGrounded);

            UpdateSpriteDirection(Direction);
        }

        protected virtual float CalculateYVelosity()
        {
            var yVelocity = Rigitbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;
            }            

            if (isJumpPressing)
            {
                _isJumping = true;
                var isFalling = Rigitbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelosity(yVelocity) : yVelocity;
            }
            else if (Rigitbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelosity(float yVelocity)
        {
            if (IsGrounded)
            {
                yVelocity += _jumpSpeed;
                Debug.Log("Jump");
                DoJumpVfx();
            }
            
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.PlayClip("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var scale = _invertScale ? -1 : 1; 
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(scale, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-scale, 1, 1);
            }
        }

        public virtual void TakeDamage()
        {
            _isJumping = false;
            Animator.SetTrigger(Hit);
            Rigitbody.velocity = new Vector2(Rigitbody.velocity.x, _damageJumpSpd);            
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            Sounds.PlayClip("Melee");
        }

        public virtual void MakeAttack()
        {
            Debug.Log("OnAttack");
            _attackRange.Check();
            _particles.Spawn("Attack");
            
        }
    }
}
