using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    //public Slider[] AudioSlider;
    string[] Parameter = { "Master", "Music", "Sfx" };

    public TMP_Dropdown ResDropDown;

    int CurrentResIndex = 0;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        ResDropDown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResIndex = i;
            }
        }

        ResDropDown.AddOptions(options);
        ResDropDown.value = CurrentResIndex;
        ResDropDown.RefreshShownValue();
    }

    public void MasterSlider(float Value)
    {
        audioMixer.SetFloat(Parameter[0], Value);
    }

    public void MusicSlider(float Value)
    {
        audioMixer.SetFloat(Parameter[1], Value);
    }

    public void SfxSlider(float Value)
    {
        audioMixer.SetFloat(Parameter[2], Value);
    }

    public void SetScrnResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen: true);
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
}
