using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    public static Sfx Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("Sfx");
                _audioSource = obj.AddComponent<AudioSource>();
                _instance = obj.AddComponent<Sfx>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    private static Sfx _instance;

    private static AudioSource _audioSource;
    private Dictionary<ESfx, AudioClip> _sfxs = new();

    public float Volume
    {
        get => _audioSource.volume;
        set
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat("SfxVolume", value);
        }
    }

    public void Initialize()
    {
        _sfxs.Clear();
        _audioSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
    }

    public void Play(ESfx sfx)
    {
        if (!_sfxs.TryGetValue(sfx, out AudioClip clip))
            _sfxs.Add(sfx, clip = Resources.Load<AudioClip>($"Audio/Sfxs/{sfx}"));

        _audioSource.PlayOneShot(clip);
    }

    public void PlayButtonClick() => Play(ESfx.Click);
}
