using UnityEngine;

namespace PixelCrew.Components
{
    public class DestroyOdjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;

        public void DestroyOdject()
        {
            Destroy(_objectToDestroy);
        }
    }
}
