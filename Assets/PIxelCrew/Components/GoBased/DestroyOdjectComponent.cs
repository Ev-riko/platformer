using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class DestroyOdjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;

        public void DestroyOdject()
        {
            //Debug.Log($"DestroyOdject: {_objectToDestroy.name}");
            Destroy(_objectToDestroy);
        }
    }
}
