using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Components.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        private PlayerData _save;
        public PlayerData Data => _data;

        private void Awake()
        {
            if (IsSessionExit())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Save();
                DontDestroyOnLoad(gameObject);
            }
            
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return true;
                }
            }
            return false;
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave() {
            _data = _save.Clone();
        }
    }
}
