using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown ResolutionDropdown;

    void Start()
    {

        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int CurrentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + "X" + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {

                CurrentResolutionIndex = i;

            }

        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int ResolutionIndex)
    {

        Resolution resolution = resolutions[ResolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);



    }

    public void SetVolume(float Volume)
    {

        audioMixer.SetFloat("Volume", Volume);

    }

    public void SetQuality(int Qualityindex)
    {

        QualitySettings.SetQualityLevel(Qualityindex);

    }

    public void SetFullScreen(bool IsFullScreen)
    {


        Screen.fullScreen = IsFullScreen;

    }

}
