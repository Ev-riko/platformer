using UnityEngine;

namespace Assets.PIxelCrew.Creatures.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private int _direction;

        private void Start()
        {
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
            _rigidbody = GetComponent<Rigidbody2D>();
            var forse = new Vector2(_speed * _direction, 0);
            _rigidbody.AddForce(forse, ForceMode2D.Impulse);
        }

        //private void FixedUpdate()
        //{
        //    var position = transform.position;
        //    position.x += _speed * _direction;
        //    _rigidbody.MovePosition(position);
        //}
    }
}
