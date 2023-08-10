using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PixelCrew.Utils
{
    public static class GameObjectExtensions
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }
    }
}
