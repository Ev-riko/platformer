using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.PIxelCrew.Components.ColliderBased
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
