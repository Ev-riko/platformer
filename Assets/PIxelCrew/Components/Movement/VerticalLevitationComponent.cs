using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLevitationComponent : MonoBehaviour
{
    [SerializeField] private float _frequency = 1f;
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private bool _randomize;

    private float _originY;
    private Rigidbody2D _rigidbody;
    private float _seed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originY = _rigidbody.position.y;
        if ( _randomize)
        {
            _seed = Random.value * 2 * Mathf.PI;
        }
    }

    void Update()
    {
        var pos = _rigidbody.position;
        pos.y = _originY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
        _rigidbody.MovePosition(pos);
    }
}
