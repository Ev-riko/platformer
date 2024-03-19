using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Totem : MonoBehaviour
{
    [SerializeField] private bool _topTotemState;
    [SerializeField] private bool _invertX;

    [SerializeField] private AnimatorController _top;
    [SerializeField] private AnimatorController _bottom;

    private Animator _animator;   

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        var scale = transform.localScale;
        scale.x *= _invertX ? -1 : 1;
        transform.localScale = scale;

        UpdateTopTotemAnimatoController();
    }

    public void SetTopTotemState(bool state)
    {
        _topTotemState = state;
        UpdateTopTotemAnimatoController();
    }

    private void UpdateTopTotemAnimatoController()
    {
        _animator.runtimeAnimatorController = _topTotemState ? _top : _bottom;
    }
}
