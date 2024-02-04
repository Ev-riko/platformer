using PixelCrew.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTrigerComponent : MonoBehaviour
    {

        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private TriggerEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{gameObject.name} trig with {other.name}");
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!String.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;

                _action?.Invoke(other.gameObject);

        }

        [Serializable]
        public class TriggerEvent : UnityEvent<GameObject>
        {
        }
    }

    
}