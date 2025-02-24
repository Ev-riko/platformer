using PixelCrew.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class OptionItemWidget : MonoBehaviour , IIttemRenderer<OptionData>
    {
        [SerializeField] private Text _label;
        [SerializeField] private SelectOption _onSelect;
        private OptionData _data;
        public void SetData(OptionData data, int index)
        {
            _data = data;
            _label.text = data.Text;
        }

        public void OnSelect()
        {
            _onSelect.Invoke(_data);
        }

        [Serializable]
        public class SelectOption : UnityEvent<OptionData>
        {

        }
    }
}
