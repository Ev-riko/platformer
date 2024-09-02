using PixelCrew.Model;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string scene;
        public void ExitLevel()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();

            SceneManager.LoadScene(scene);
        }       
    }
}