using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject go)
        {
            var interactble = go.GetComponent<InteractableComponent>();
            if (interactble != null)
            {
                interactble.Interact();
            }
        }
    }
}
