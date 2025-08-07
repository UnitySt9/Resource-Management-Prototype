using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        private const string MUSIC_PREF_KEY = "MusicVolume";
        private const string SFX_PREF_KEY = "SFXVolume";
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _musicVolumeParam = "MusicVolume";
        [SerializeField] private string _sfxVolumeParam = "SFXVolume";
        [SerializeField] private float _defaultMusicVolume = 0.8f;
        [SerializeField] private float _defaultSFXVolume = 0.8f;

        private void Start()
        {
            float musicVolume = PlayerPrefs.GetFloat(MUSIC_PREF_KEY, _defaultMusicVolume);
            float sfxVolume = PlayerPrefs.GetFloat(SFX_PREF_KEY, _defaultSFXVolume);
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
        }

        private void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(_musicVolumeParam, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
            PlayerPrefs.SetFloat(MUSIC_PREF_KEY, volume);
        }

        private void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat(_sfxVolumeParam, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
            PlayerPrefs.SetFloat(SFX_PREF_KEY, volume);
        }

        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MUSIC_PREF_KEY, _defaultMusicVolume);
        }

        public float GetSFXVolume()
        {
            return PlayerPrefs.GetFloat(SFX_PREF_KEY, _defaultSFXVolume);
        }
    }
}