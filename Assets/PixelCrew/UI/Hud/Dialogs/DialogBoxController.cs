using PixelCrew.Model.Data;
using PixelCrew.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")]
        [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private static readonly int IsOpen = Animator.StringToHash("is-open");

        private DialogData _data;
        private int _currentSintence;
        private AudioSource _sfxSource;
        private Coroutine _typingCoroutine;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSintence = 0;
            _text.text = string.Empty;

            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);

            _animator.SetBool(IsOpen, true);
            Debug.Log("IsOpen");

        }

        public void OnSkip()
        {
            if (_typingCoroutine == null) return;

            StopTypeAnimation();
        }

        private void StopTypeAnimation()
        {
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _text.text = _data.Sentences[_currentSintence];
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSintence++;

            var isDialogComplited = _currentSintence >=_data.Sentences.Length;
            if (isDialogComplited)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource?.PlayOneShot(_close);
        }

        private void OnStartDialogAnimation()
        {
            _typingCoroutine = StartCoroutine(TypeDialogText());
        }

        private IEnumerator TypeDialogText()
        {
            _text.text = string.Empty;
            var sentence = _data.Sentences[_currentSintence];
            foreach (var letter in sentence) 
            {
                _text.text += letter;
                _sfxSource?.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingCoroutine = null;
        }

        private void OnCloseDialogAnimation()
        {
            _container.SetActive(false);
        }
    }
}
