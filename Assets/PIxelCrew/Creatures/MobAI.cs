using PixelCrew;
using PixelCrew.Components;
using PixelCrew.Creatures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;
    [SerializeField] private LayerCheck _canAttack;

    [SerializeField] private float _alarmDelay = 0.5f;
    [SerializeField] private float _attackCooldown = 1f;

    private Coroutine _current;
    private GameObject _target;

    private SpawnListComponent _particles;
    private Creature _creature;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
    }

    private void Start()
    {
        StartState(Patrolling());
    }

    public void OnHeroInVision(GameObject go)
    {
        Debug.Log("OnHeroInVision");
        _target = go;
        StartState(AgroToHero());
    }

    private IEnumerator AgroToHero()
    {
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds(_alarmDelay);

        StartState(GoToHero());
    }

    private IEnumerator GoToHero()
    {
        while (_vision.IsTouchingLayer)
        {
            Debug.Log("GoToHero");
            if (_canAttack.IsTouchingLayer)
            {
                StartState(Attack());
            }
            else
            {
                SetDirectionToTarget();
            } 
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (_canAttack.IsTouchingLayer)
        {
            _creature.Attack();
            yield return new WaitForSeconds(_attackCooldown);
        }

        StartState(GoToHero());
    }

    private void SetDirectionToTarget()
    {
        Debug.Log("SetDirectionToTarget");
        var direction = _target.transform.position - transform.position;
        direction.Normalize();
        _creature.SetDirection(new Vector2(direction.x, 0));
    }

    private IEnumerator Patrolling()
    {
        yield return null;
    }

    private void StartState(IEnumerator coroutine)
    {
        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(coroutine);
    }
}
