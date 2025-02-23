using PixelCrew.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.MainMenu
{
    public class MainManuWindow : AnimatedWindow
    {
        private Action _closeAction;
        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif 
            };
            Close();
        }

        public override void OnCloseAnimationCompilete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationCompilete();
        }
    }
}
