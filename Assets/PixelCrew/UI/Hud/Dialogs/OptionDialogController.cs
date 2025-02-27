using JetBrains.Annotations;
using PixelCrew.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {

        [SerializeField] private GameObject _content;
        [SerializeField] private Text _contentText;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _optionsContainer);
        }

        public void OnOptionsSelected(OptionData selectedOptions)
        {
            selectedOptions.OnSelect.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionsDialogData data)
        {
            _content.SetActive(true);
            _contentText.text = data.DialogText;

            _dataGroup.SetData(data.Options);
        }
    }

    [Serializable]
    public class OptionsDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }


    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}