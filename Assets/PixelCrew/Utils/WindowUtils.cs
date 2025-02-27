using TMPro;
using UnityEngine;

namespace PixelCrew.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            //var canvas = Object.FindObjectOfType<Canvas>();
            var canvases = Object.FindObjectsOfType<Canvas>();

            var mainCanvasTag = "Player";

            foreach (var canvas in canvases) 
            {
                if (canvas.CompareTag(mainCanvasTag)) {
                    Object.Instantiate(window, canvas.transform);
                    return;
                }
            }
        }
    }
}
