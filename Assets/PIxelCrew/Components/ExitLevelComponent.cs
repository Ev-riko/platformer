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
            SceneManager.LoadScene(scene.name);
        }
    }
}