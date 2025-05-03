using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;        
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    const float minDb = -80f, maxDb = 0f;

    void OnEnable()
    {
        // grab current values from the mixer and push into sliders
        mixer.GetFloat("MasterVolume", out var m);
        mixer.GetFloat("MusicVolume", out var u);
        mixer.GetFloat("SFXVolume", out var s);

        masterSlider.value = Mathf.InverseLerp(minDb, maxDb, m);
        musicSlider.value = Mathf.InverseLerp(minDb, maxDb, u);
        sfxSlider.value = Mathf.InverseLerp(minDb, maxDb, s);

        // wire up the callbacks
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void OnDisable()
    {
        // clean up so we don’t double-subscribe
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    public void SetMasterVolume(float t) => mixer.SetFloat("MasterVolume", Mathf.Lerp(minDb, maxDb, t));
    public void SetMusicVolume(float t) => mixer.SetFloat("MusicVolume", Mathf.Lerp(minDb, maxDb, t));
    public void SetSFXVolume(float t) => mixer.SetFloat("SFXVolume", Mathf.Lerp(minDb, maxDb, t));
}
