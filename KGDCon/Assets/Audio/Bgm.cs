using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public static Bgm Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("Bgm");
                _audioSource = obj.AddComponent<AudioSource>();
                _audioSource.loop = true;
                _instance = obj.AddComponent<Bgm>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    private static Bgm _instance;

    public bool IsPlaying => _audioSource.isPlaying;

    private static AudioSource _audioSource;
    private Dictionary<EBgm, AudioClip> _bgms = new();

    public float Volume
    {
        get => _audioSource.volume;
        set
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat("BgmVolume", value);
        }
    }

    public void Initialize()
    {
        _bgms.Clear();
        _audioSource.volume = PlayerPrefs.GetFloat("BgmVolume", 1f);
    }

    public void Play(EBgm bgm)
    {
        if (!_bgms.TryGetValue(bgm, out AudioClip clip))
            _bgms.Add(bgm, clip = Resources.Load<AudioClip>($"Audio/Bgms/{bgm}"));

        if (clip == _audioSource.clip)
            return;

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void Stop()
    {
        if (!_audioSource.isPlaying)
            return;

        _audioSource.Stop();
    }
}
