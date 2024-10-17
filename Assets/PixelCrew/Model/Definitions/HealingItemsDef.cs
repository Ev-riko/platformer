using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/HeelingItems", fileName = "HeelingItems")]
    public class HealingItemsDef : ScriptableObject
    {
        [SerializeField] private HealingDef[] _items;

        public HealingDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }
            return default;
        }
    }

    [Serializable]
    public struct HealingDef
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _power;

        public string Id => _id;
        public int Power => _power;
    }

}
