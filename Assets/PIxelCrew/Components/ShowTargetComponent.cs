using PixelCrew.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTargetComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraStateController _controller;
    [SerializeField] private float _delay;

    private void OnValidate()
    {
        if (_controller == null)
            _controller = FindObjectOfType<CameraStateController>();
    }

    public void ShowTarget()
    {
        _controller.SetPosition(_target.position);
        _controller.SetState(true);
        Invoke(nameof(MoveBack), _delay);
    }

    private void MoveBack()
    {
        _controller.SetState(false);
    }
}
