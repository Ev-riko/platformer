using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public void PlayClip(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.Id != id) continue;

            _source.PlayOneShot(audioData.Clip);
            break;
        }
    }


    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}
