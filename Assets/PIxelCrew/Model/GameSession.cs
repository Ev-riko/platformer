﻿using PixelCrew.Model.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        private PlayerData _save;
        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }

        

        private void Awake()
        {
            LoadHud();

            if (IsSessionExit())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(gameObject);
            }
            
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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
