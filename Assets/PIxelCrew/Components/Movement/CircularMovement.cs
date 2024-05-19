using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class CircularMovement : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _speed = 1f;
        private Rigidbody2D[] _bodies;
        private Vector2[] _positions;
        private float _time;

        private void Awake()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            _bodies = GetComponentsInChildren<Rigidbody2D>();
            _positions = new Vector2[_bodies.Length];
        }

        void Update()
        {
            CalculatePositions();

            var isAllDead = true;
            for (int i = 0; i < _bodies.Length; i++)
            {
                if (_bodies[i])
                {
                    _bodies[i].MovePosition(_positions[i]);
                    isAllDead = false;
                }
            }
            _time += Time.deltaTime;

            if (isAllDead)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
        }

        private void CalculatePositions()
        {
            var step = 2 * Mathf.PI / _bodies.Length;

            Vector2 containerPosition = transform.position;
            for (int i = 0; i < _bodies.Length; i++)
            {
                var angle = step * i;
                var pos = new Vector2(
                    Mathf.Cos(angle + _time * _speed),
                    Mathf.Sin(angle + _time * _speed)
                    ) * _radius;
                _positions[i] = pos + containerPosition;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateContent();
            CalculatePositions();
            for (int i = 0; i < _bodies.Length; i++)
            {
                _bodies[i].transform.position = _positions[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
        }
#endif
    }
}