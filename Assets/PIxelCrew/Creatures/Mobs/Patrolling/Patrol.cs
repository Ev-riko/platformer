
using System.Collections;
using UnityEngine;

namespace Assets.PIxelCrew.Creatures
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
