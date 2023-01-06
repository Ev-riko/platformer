using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;


        private Vector2 _direction;
        private Rigidbody2D _rigitbody;

        private void Awake()
        {
            _rigitbody = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigitbody.velocity = new Vector2(_direction.x * _speed, _rigitbody.velocity.y);

            var isLumping = _direction.y > 0;
            if (isLumping)
            {
                if (IsGrounded() && _rigitbody.velocity.y <= 0)
                {
                    _rigitbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_rigitbody.velocity.y > 0)
            {
                _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, _rigitbody.velocity.y * 0.5f);
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
    }
}
