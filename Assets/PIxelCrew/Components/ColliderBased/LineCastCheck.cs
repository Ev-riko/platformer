using UnityEditor;
using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class LineCastCheck : LayerCheck
    {
        [SerializeField] private Transform _target;

        private readonly RaycastHit2D[] _result = new RaycastHit2D[1];

        private void Update()
        {
            _isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, _layer) > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.DrawLine(transform.position, _target.position);
        }
#endif

    }
}
