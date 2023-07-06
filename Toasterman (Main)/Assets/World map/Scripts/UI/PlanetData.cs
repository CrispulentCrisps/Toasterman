using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlanetData : MonoBehaviour
{
    Data planetData;

    [SerializeField] private GameObject Glint;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Image CurrentImage;
    [SerializeField] private Sprite[] BoxSprites;
    [SerializeField] private int TextPos;

    bool[] TEMP_PlanetsCompleted = { false, false, false };
    float[] TEMP_ScoreForPlanet = { 0, 0, 0 };
    int[] TEMP_TimesLost = { 0, 0, 0 };
    int[] TEMP_TimesPlayed = { 0, 0, 0 };
    bool[] TEMP_DevScoreBeat = { false, false, false };

    string path;

    public void LoadData()
    {
        path = Application.dataPath + "/SaveData/SaveData.json";
        planetData = JsonUtility.FromJson<Data>(File.ReadAllText(path));

        TEMP_PlanetsCompleted[TextPos] = planetData.PlanetsCompleted[TextPos];
        TEMP_ScoreForPlanet[TextPos] = planetData.ScoreForPlanet[TextPos];
        TEMP_TimesLost[TextPos] = planetData.TimesLost[TextPos];
        TEMP_TimesPlayed[TextPos] = planetData.TimesPlayed[TextPos];
        TEMP_DevScoreBeat[TextPos] = planetData.DevScoreBeat[TextPos];
    }

    void Start()
    {
        LoadData();
        textDisplay.text = "";
        //Aced
        if (TEMP_DevScoreBeat[TextPos] == true)
        {
            CurrentImage.sprite = BoxSprites[2];
            Glint.active = true;
            textDisplay.text += "Level: Aced \n";
        }
        //Passed
        else if (TEMP_PlanetsCompleted[TextPos] == true)
        {
            CurrentImage.sprite = BoxSprites[1];
            Glint.active = false;
            textDisplay.text += "Level: Beat \n";
        }
        //Failed
        else
        {
            CurrentImage.sprite = BoxSprites[0];
            Glint.active = false;
            textDisplay.text += "Level: Not Beat \n";
        }
        textDisplay.text += "Highscore: " + TEMP_ScoreForPlanet[TextPos] + " \n";
        textDisplay.text += "Times Lost: " + TEMP_TimesLost[TextPos] + " \n";
        textDisplay.text += "Times Played: " + TEMP_TimesPlayed[TextPos] + " \n";

    }
}
