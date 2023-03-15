using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    public GameObject menuPanel;
    public GameObject optionsPanel;

    private SaveOptions saveOptions = new SaveOptions();

    public AudioSource openClosePanelSource;

    public AudioMixerGroup mixerGroup;
    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        _SFXSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });

        if (PlayerPrefs.HasKey("MusicOptions"))
        {
            saveOptions = JsonUtility.FromJson<SaveOptions>(PlayerPrefs.GetString("Options"));

            _musicSlider.value = saveOptions.musicValue;
            _SFXSlider.value = saveOptions.SFXValue;
        }
        else
        {
            DefaultOptions();
        }
    }

    private void Start()
    {
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void DefaultOptions()
    {
        _musicSlider.value = 0.65f;
        _SFXSlider.value = 0.67f;

        ChangeMusicVolume();
        ChangeSFXVolume();
    }
    public void ChangeMusicVolume()
    {
        mixerGroup.audioMixer.SetFloat("Music", Mathf.Lerp(-88, 0, _musicSlider.value));
    }

    public void ChangeSFXVolume()
    {
        mixerGroup.audioMixer.SetFloat("SFX", Mathf.Lerp(-88, 0, _SFXSlider.value));
    }

    public void OpenCloseOptionsPanel()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);

        openClosePanelSource.Play();
    }
    public void SaveOptions()
    {
        saveOptions.musicValue = _musicSlider.value;
        saveOptions.SFXValue = _SFXSlider.value;

        PlayerPrefs.SetString("Options", JsonUtility.ToJson(saveOptions));

        openClosePanelSource.Play();

    }
    public void ResetOptions()
    {
        PlayerPrefs.DeleteKey("Options");

        DefaultOptions();

        openClosePanelSource.Play();
    }
}

[Serializable]
public class SaveOptions
{
    public float musicValue;
    public float SFXValue;
}