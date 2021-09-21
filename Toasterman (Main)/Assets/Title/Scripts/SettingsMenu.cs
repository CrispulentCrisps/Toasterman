using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    GameObject scanlines;

    public Slider ScanSlide;

    public Toggle[] toggles;
    
    public UnityEngine.Rendering.VolumeProfile profile;

    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown ResolutionDropdown;

    private void OnLevelWasLoaded()
    {
        scanlines = GameObject.Find("Scanlines 1");

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

        ScanlinesSettings.Opacity = ScanSlide.value;

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)//Checks if on
            {
                switch (i)//Goes through to find which one is on in the array
                {
                    case 0:
                        SetBloom(true);
                        break;
                    case 1:
                        SetLens(true);
                        break;
                }
            }
            else
            {
                switch (i)//Goes through to find which one is on in the array
                {
                    case 0:
                        SetBloom(false);
                        break;
                    case 1:
                        SetLens(false);
                        break;
                }
            }
        }
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float Volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(Volume) * 20);
    }

    public void SetQuality(int Qualityindex)
    {
        QualitySettings.SetQualityLevel(Qualityindex);
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

    public void SetBloom(bool Set)
    {
        UnityEngine.Rendering.Universal.Bloom bloom;
        profile.TryGet(out bloom);
        bloom.active = Set;
    }

    public void SetScanlines(float opaque)
    {
        ScanlinesSettings.Opacity = opaque;
    }

    public void SetLens(bool Set)
    {
        UnityEngine.Rendering.Universal.LensDistortion ld;
        profile.TryGet(out ld);
        ld.active = Set;
    }
}