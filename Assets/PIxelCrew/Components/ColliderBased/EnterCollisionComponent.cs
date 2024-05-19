using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class EnterCollisionComponent : MonoBehaviour
    {

        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Collision");
            if (other.gameObject.CompareTag(_tag))
                _action?.Invoke(other.gameObject);
            
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}