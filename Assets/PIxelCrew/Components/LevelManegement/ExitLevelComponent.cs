using PixelCrew.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private SceneAsset scene;
        public void ExitLevel()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();

            SceneManager.LoadScene(scene.name);
        }       
    }
}