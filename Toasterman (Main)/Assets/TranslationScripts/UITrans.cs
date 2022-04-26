using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translatedtext
{
    public string[] Words;
}

public class UITrans : MonoBehaviour
{
    Translatedtext txt;

    public int TextPos;
    public int LanguageChoice;
    
    const int ENGLISH = 0;
    const int GERMAN = 1;
    const int NSPANISH = 2;
    const int ARGENTINIAN = 3;
    const int PORTUGUESE = 4;

    void GetLanguageFromJSON(int Language, int Pos)
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
                path += "German/toast_settings_gr";
                break;
        }

        Debug.Log(path);
        txt = JsonUtility.FromJson<Translatedtext>(File.ReadAllText(path));
    }
    void Start()
    {
        LanguageChoice = ENGLISH;
        GetLanguageFromJSON(LanguageChoice, TextPos);
    }
}
