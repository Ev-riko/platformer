using System.Xml;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Space _space;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            //Debug.Log("Spawn");
            if (_space == Space.World)
            {
                var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
                instantiate.transform.localScale = _target.lossyScale;
            }
            else
            {
                Instantiate(_prefab, _target.position, Quaternion.identity, _target);
            }
            
        }
    }
}
