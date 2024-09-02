using PixelCrew.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverlap;
        private Collider2D[] _interactionResult = new Collider2D[10];

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult, 
                _mask);

            var overlaps = new List<GameObject>();

            for (int i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var IsInTAgs = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (IsInTAgs)
                {
                    _onOverlap?.Invoke(overlapResult.gameObject);
                }
                
            }

        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {

        }
    }


}
