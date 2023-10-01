using PixelCrew.Creatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PIxelCrew.Creatures
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _treshold = 1f;

        private Creature _creature;
        private int _destinationPointIndex;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }
        public override IEnumerator DoPatrol()
        {
            while (enabled) {
                if (IsOnPoint())
                {
                    _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
                }
                var direction = _points[_destinationPointIndex].position - transform.position;
                direction.y = 0f;
                Debug.Log(direction.normalized);
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
        }

        private bool IsOnPoint()
        {
            var magnitude = (_points[_destinationPointIndex].position - transform.position).magnitude;
            Debug.Log(magnitude);
            return magnitude < _treshold;
        }
    }
}
