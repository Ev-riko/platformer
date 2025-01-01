using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Hud.Dialogs;
using System;
using UnityEngine;

namespace PixelCrew.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        private DialogBoxController _dialogBox;

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound: 
                        return _bound;
                    case Mode.External: 
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindObjectOfType<DialogBoxController>();
            Debug.Log(_dialogBox);

            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def) {
            _external = def;
            Show();
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}
