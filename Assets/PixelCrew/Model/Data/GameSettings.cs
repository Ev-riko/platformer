using PixelCrew.Model.Data.Properties;
using System;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [CreateAssetMenu(menuName = "Defs/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty Music;
        [SerializeField] private FloatPersistentProperty Sfx;

        private static GameSettings _instance;
        public static GameSettings I => _instance ?? LoadGameSettings();

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            Music = new FloatPersistentProperty(1, SoundSetings.Music.ToString());
            Sfx = new FloatPersistentProperty(1, SoundSetings.Sfx.ToString());
        }

        private void OnValidate()
        {
            Music.Validate();
            Sfx.Validate();
        }
    }

    public enum SoundSetings
    {
        Music,
        Sfx
    }
}
