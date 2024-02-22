using Assets.PIxelCrew.Components.ColliderBased;
using Assets.PIxelCrew.Creatures;
using PixelCrew;
using PixelCrew.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrol : Patrol
{

    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private LayerCheck _obstacleCheck;
    [SerializeField] private Creature _creature;
    [SerializeField] private int _direction;

    public override IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (_groundCheck.IsTouchingLayer && !_obstacleCheck.IsTouchingLayer)
            {
                _creature.SetDirection(new Vector2(_direction, 0));
            }
            else
            {
                _direction = -_direction;
                _creature.SetDirection(new Vector2(_direction, 0));
            }

            yield return null;
        }
    }
}
