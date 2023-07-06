using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Translatedtext
{
    public string[] Words;
}

public class UITrans : MonoBehaviour
{
    Translatedtext txt;
    SettingsData sd;

    public TextMeshProUGUI textDisplay;

    public int TextPos;
    public int LanguageChoice;
    
    const int ENGLISH = 0;
    const int GERMAN = 1;
    const int NSPANISH = 2;
    const int ARGENTINIAN = 3;
    const int PORTUGUESE = 4;

    public void GetLanguageFromJSON(int Language)
    {
        string path = Application.streamingAssetsPath + "/TextStuff/";
        switch (Language)
        {
            default:
                path += "English/toast_settings_en.json";
                break;
            case ENGLISH:
                path += "English/toast_settings_en.json";
                break;
            case GERMAN:
                path += "German/toast_settings_gr.json";
                break;
        }

        Debug.Log(path);
        txt = JsonUtility.FromJson<Translatedtext>(File.ReadAllText(path));
        textDisplay.text = txt.Words[TextPos];
    }

    public void InitLanguage()
    {
        string path = Application.streamingAssetsPath + "/settings.json";
        Debug.Log(path);
        sd = JsonUtility.FromJson<SettingsData>(File.ReadAllText(path));
        LanguageChoice = sd.Language;
    }

    void Awake()
    {
        gameObject.tag = "Text";
        InitLanguage();
    }

    public void ChangeLanguage(int Language)
    {
        GetLanguageFromJSON(Language);
    }
}
