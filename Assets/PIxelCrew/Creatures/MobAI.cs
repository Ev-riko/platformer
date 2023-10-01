using Assets.PIxelCrew.Creatures;
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
    [SerializeField] private float _MissHeroCooldown = 1f;

    private static readonly int IsDeadKey = Animator.StringToHash("is-dead"); 

    private Coroutine _current;
    private GameObject _target;

    private SpawnListComponent _particles;
    private Creature _creature;
    private Animator _animator;
    private bool _isDead = false;
    private Patrol _patrol;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
        _patrol = GetComponent<Patrol>();
    }

    private void Start()
    {
        StartState(_patrol.DoPatrol());
    }

    public void OnHeroInVision(GameObject go)
    {
        if (_isDead) return;

        Debug.Log("OnHeroInVision");
        _target = go;
        StartState(AgroToHero());
    }

    private IEnumerator AgroToHero()
    {
        Debug.Log("Exclamation");
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds(_alarmDelay);

        StartState(GoToHero());
    }

    private IEnumerator GoToHero()
    {
        while (_vision.IsTouchingLayer)
        {
            //Debug.Log("GoToHero");
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

        _particles.Spawn("MissHero");
        yield return new WaitForSeconds(_MissHeroCooldown);
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
        //Debug.Log("SetDirectionToTarget");
        var direction = _target.transform.position - transform.position;
        direction.y = direction.z = 0;
        direction.Normalize();
        _creature.SetDirection(direction);
    }
    private void StartState(IEnumerator coroutine)
    {
        _creature.SetDirection(Vector2.zero);

        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(coroutine);
    }

    public void OnDie()
    {
         _isDead = true;
        _animator.SetBool(IsDeadKey, true);

        if (_current != null)
            StopCoroutine(_current);
    }
}
