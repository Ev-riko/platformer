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
        private PlayerData _startData;
        public PlayerData Data { get { return _data; } }

        private void Awake()
        {
            if (_startData == null)
            {
                _startData = new PlayerData(0, 5, false);
            }


            if (IsSessionExit())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
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

        public void SetStartData()
        {            
            _startData.Copy(_data);
        }

        public void ReloadData()
        {
            _data.Copy(_startData);
        }
    }
}
