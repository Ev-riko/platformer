using Assets.PIxelCrew.Model.Definitions;
using PixelCrew.Creatures;
using UnityEngine;

namespace Assets.PIxelCrew.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _value;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddInInventory(_id, _value);
            }
        }
    }
}
