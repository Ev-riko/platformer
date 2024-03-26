using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PIxelCrew.Model
{
    [CreateAssetMenu(menuName = "Defs/Inventoryitems",  fileName = "Inventoryitems")]
    public class InventoryItemsDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if(itemDef.Id == id)
                    return itemDef;
            }
            return default;
        }

#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => _items;
#endif
    }



    [Serializable]
    public class ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStackable;
        public string Id => _id;
        public bool IsStackable => _isStackable;
        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
