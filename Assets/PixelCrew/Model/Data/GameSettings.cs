using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [CreateAssetMenu(menuName = "Defs/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings I => _instance ?? LoadGameSettings();

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSetings.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSetings.Sfx.ToString());
        }

        private void OnValidate()
        {
            _music.Validate();
            _sfx.Validate();
        }
    }

    public enum SoundSetings
    {
        Music,
        Sfx
    }
}
