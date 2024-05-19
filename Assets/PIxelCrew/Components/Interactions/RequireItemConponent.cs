using PixelCrew.Model;
using PixelCrew.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class RequireItemConponent : MonoBehaviour
    {

        [SerializeField] private InventoryItemData[] _required;
        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _onSucces;
        [SerializeField] private UnityEvent _onFail;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementsMet = true;
            foreach (var item in _required)
            {
                var numItems = session.Data.inventory.Count(item.Id);
                if (numItems < item.Value)
                    areAllRequirementsMet = false;
            }


            if (areAllRequirementsMet)
            {
                if (_removeAfterUse)
                    foreach (var item in _required)
                        session.Data.inventory.Remove(item.Id, item.Value);
                _onSucces?.Invoke(); 
            }
            else
            {
                _onFail?.Invoke();
            }
        }
    }
}
