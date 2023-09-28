
using System;
using Unity.VisualScripting;
using UnityEngine;

public class DropSpawnComponent : MonoBehaviour
{

    [SerializeField] private float _startImpulse;
    [SerializeField] private Loot[] _loot;
    private Transform _transform;


    private void Awake()
    {
        _transform = transform;
    }

    public void SpawnDrops()
    {
        foreach (var loot in _loot)
        {
            for (var i = 0; i < loot.MaxQuantity; i++)
            {
                if (UnityEngine.Random.Range(0, 1.0f) < loot.DropChance)
                {
                    var go = Instantiate(loot.GameObject, _transform.position, _transform.rotation);
                    var randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
                    randomDirection.y = Mathf.Abs(randomDirection.y);
                    go?.GetComponent<Rigidbody2D>().AddForce(randomDirection * _startImpulse, ForceMode2D.Impulse);
                }
            }
        }
    }


}

[Serializable]
class Loot
{
    [SerializeField] private GameObject _gameObject;
    [Range(0, 1)]
    [SerializeField] private float _dropChance;
    [SerializeField] private float _maxQuantity;

    public GameObject GameObject { get { return _gameObject; } }
    public float DropChance { get { return _dropChance; } set { _dropChance = Mathf.Min(Mathf.Max(value, 1), 0); } }
    public float MaxQuantity { get { return _maxQuantity; } }
}