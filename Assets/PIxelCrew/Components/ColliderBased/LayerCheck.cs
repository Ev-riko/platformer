using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PIxelCrew.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchingLayer;
        public bool IsTouchingLayer => _isTouchingLayer;
    }
}
