using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> _musicSounds;
    [SerializeField] private List<Sound> _sfxSounds;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    private void Start()
    {
        PlayMusic("Ambient Loop");
        PlayMusic("Wind Loop");
    }

    public void PlayMusic(string name)
    {
        Sound sound = _musicSounds.Find(s => s.Name == name);

        if (sound != null)
        {
            _musicSource.clip = sound.Clip;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = _sfxSounds.Find(s => s.Name == name);

        if (sound != null)
        {
            _sfxSource.PlayOneShot(sound.Clip);
        }
    }
}
