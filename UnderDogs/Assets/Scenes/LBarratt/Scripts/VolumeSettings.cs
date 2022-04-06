using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] float _multiplier = 30f;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_MASTER = "MasterVolume";

    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(MIXER_MASTER, masterSlider.value);
        PlayerPrefs.SetFloat(MIXER_SFX, sfxSlider.value);
        PlayerPrefs.SetFloat(MIXER_MUSIC, musicSlider.value);
    }

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(MIXER_MASTER, masterSlider.value);
        sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX, sfxSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC, musicSlider.value);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * _multiplier);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * _multiplier);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * _multiplier);
    }
}
