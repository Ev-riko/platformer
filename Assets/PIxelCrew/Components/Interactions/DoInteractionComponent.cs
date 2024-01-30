using PixelCrew.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PIxelCrew.Components
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
