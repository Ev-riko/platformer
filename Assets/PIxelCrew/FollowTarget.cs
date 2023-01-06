using UnityEngine;

namespace PixelCrew
{


    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            var detination = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = detination;
        }
    }
}
