using Assets.PIxelCrew.Components.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private SceneAsset scene;
        public void ExitLevel()
        {
            FindObjectOfType<GameSession>().SetStartData();
            SceneManager.LoadScene(scene.name);
        }       
    }
}