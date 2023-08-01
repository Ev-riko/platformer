using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [Range(0, 30)][SerializeField] private int _frameRate = 10;
        [SerializeField] private AnimationClip[] _clips;
        [SerializeField] private UnityEvent<string> _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentFrame;
        private float _nextFrameTime;

        private int _currentClipIndex;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();

            _secondsPerFrame = 1f / _frameRate;

            _currentClipIndex = 0;

            StartAnimation();
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time + _secondsPerFrame;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            var clip = _clips[_currentClipIndex];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    clip.OnComplete?.Invoke();
                    _onComplete?.Invoke(clip.Name);
                    enabled = clip.AllowNextClip;
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClipIndex = (int)Mathf.Repeat(_currentClipIndex + 1, _clips.Length);
                    }
                    return;
                }
            }

            _renderer.sprite = clip.Sprites[_currentFrame];
            _nextFrameTime += _secondsPerFrame;
            _currentFrame++;
            //Debug.Log($"_currentSpriteIndex: {_currentFrame}");
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time + _secondsPerFrame;
            _currentFrame = 0;
        }

        public void SetClip(string clipName)
        {
            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClipIndex = i;
                    StartAnimation();
                    return;
                }
            }
            enabled = false;
        }

    }


    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool Loop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplete => _onComplete;

    }
}