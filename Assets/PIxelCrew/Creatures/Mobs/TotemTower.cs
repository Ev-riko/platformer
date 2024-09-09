using PixelCrew.Components.Health;
using PixelCrew.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour
    {

        [SerializeField] private List<ShootingTrapAI> _traps;
        [SerializeField] private Cooldown _cooldown;

        private bool[] _aliveTraps;
        private int _currentTrap;

        private void Start()
        {
            _aliveTraps = new bool[_traps.Count];
            _aliveTraps.Select(x => true);

            foreach (var shootingTrapAI in _traps)
            {
                shootingTrapAI.enabled = false;
                var hp = shootingTrapAI.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDead(shootingTrapAI));
            }
            UpdateTopTotemAnimator();
        }

        private void OnTrapDead(ShootingTrapAI shootingTrapAI)
        {
            var index = _traps.IndexOf(shootingTrapAI);
            _traps.RemoveAt(index);
            if (index < _currentTrap)
            {
                _currentTrap = (int)Mathf.Repeat(_currentTrap - 1, _traps.Count); ;
            }
            if (_traps.Count != 0)
                UpdateTopTotemAnimator();
        }

        private void UpdateTopTotemAnimator()
        {
            var indexOfTopTotem = 0;
            var positionYOfTopTotem = _traps[indexOfTopTotem].transform.position.y;
            for (int i = 1; i < _traps.Count; i++)
            {
                var tempY = _traps[i].transform.position.y;
                if (tempY > positionYOfTopTotem)
                {
                    positionYOfTopTotem = tempY;
                    indexOfTopTotem = i;
                }
            }
            var topTotem = _traps[indexOfTopTotem].GetComponent<Totem>();
            topTotem.SetTopTotemState(true);
        }

        private void Update()
        {
            if (_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarget = _traps.Any(x => x._vision.IsTouchingLayer);

            if (hasAnyTarget)
            {
                if (_cooldown.IsReady)
                {
                    _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                    if (_traps[_currentTrap] != null)
                        _traps[_currentTrap].RangeAttack();
                    _cooldown.Reset();
                }
            }
        }
    }
}
