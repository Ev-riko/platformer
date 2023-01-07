using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private int _coins;

        private Vector2 _direction;
        private Rigidbody2D _rigitbody;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        
        private void Awake()
        {
            _rigitbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigitbody.velocity = new Vector2(_direction.x * _speed, _rigitbody.velocity.y);

            var isLumping = _direction.y > 0;
            var isGrounded = IsGrounded();

            if (isLumping)
            {
                if (isGrounded && _rigitbody.velocity.y <= 0)
                {
                    _rigitbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_rigitbody.velocity.y > 0)
            {
                _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, _rigitbody.velocity.y * 0.5f);
            }

            _animator.SetFloat(VerticalVelocityKey, _rigitbody.velocity.y);
            _animator.SetBool(IsRunningKey, _direction.x != 0);
            _animator.SetBool(IsGroundKey, isGrounded);

            UpdateSpriteDirection();
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;
            }
        }

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }

        public void SaySomething()
        {
            Debug.Log("Something");
        }
        public void AddCoin(int CoinsNumber)
        {
            _coins += CoinsNumber;
            Debug.Log(_coins);
        }
    }
}
