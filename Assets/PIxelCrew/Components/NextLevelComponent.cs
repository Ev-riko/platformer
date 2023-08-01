using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelComponent : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;

    public void NextLevel()
    {
        SceneManager.LoadScene(scene.name);
    }
}
