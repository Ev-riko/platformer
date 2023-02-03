using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTrigerComponent : MonoBehaviour
    {

        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _action?.Invoke();
        }
    }
}