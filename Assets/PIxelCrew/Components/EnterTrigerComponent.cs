using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTrigerComponent : MonoBehaviour
    {

        [SerializeField] private string _tag;
        [SerializeField] private TriggerEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _action?.Invoke(other.gameObject);
        }

        [Serializable]
        public class TriggerEvent : UnityEvent<GameObject>
        {
        }
    }

    
}