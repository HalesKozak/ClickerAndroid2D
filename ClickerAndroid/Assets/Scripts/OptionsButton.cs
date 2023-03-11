using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    public GameObject menuPanel;
    public GameObject optionsPanel;

    public AudioSource openClosePanelSource;

    public AudioMixerGroup mixerGroup;

    public void Start()
    {
        _musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        _SFXSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
        mixerGroup.audioMixer.SetFloat("Music", -36);
        mixerGroup.audioMixer.SetFloat("SFX", -36);
    }

    public void ChangeMusicVolume()
    {
        mixerGroup.audioMixer.SetFloat("Music", Mathf.Lerp(-88, 10, _musicSlider.value));
    }

    public void ChangeSFXVolume()
    {
        mixerGroup.audioMixer.SetFloat("SFX", Mathf.Lerp(-88, 10, _SFXSlider.value));
    }

    public void OpenCloseOptionsPanel()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        openClosePanelSource.Play();
    }
}
