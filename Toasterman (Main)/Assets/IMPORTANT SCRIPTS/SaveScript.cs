using UnityEngine;
using System.IO;

[System.Serializable]
public class Data 
{
    public bool[] PlanetsCompleted = { false, false };
    public int[] ScoreForPlanet = { 0, 0 };
    public int[] TimesLost = { 0, 0 };
    public int[] TimesPlayed = { 0, 0 };
    public bool[] DevScoreBeat = { false, false };
}
public class SaveScript : MonoBehaviour
{
    public bool[] TEMP_PlanetsCompleted = { false, false };
    public int[] TEMP_ScoreForPlanet = { 0, 0 };
    public int[] TEMP_TimesLost = { 0, 0 };
    public int[] TEMP_TimesPlayed = { 0, 0 };
    public bool[] TEMP_DevScoreBeat = { false, false };
    Data data;
    string path;

    void Start()
    {
        path = Application.dataPath + "/SaveData/SaveData.json";
        Debug.Log(path);
        LoadData();
    }

    public void SaveData()
    {
        Data NewData = new Data();
        NewData.PlanetsCompleted = TEMP_PlanetsCompleted;
        NewData.ScoreForPlanet = TEMP_ScoreForPlanet;
        NewData.TimesLost = TEMP_TimesLost;
        NewData.TimesPlayed = TEMP_TimesPlayed;
        NewData.DevScoreBeat = TEMP_DevScoreBeat;
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(NewData));
    }

    public void LoadData()
    {
        data = JsonUtility.FromJson<Data>(File.ReadAllText(path));
        TEMP_PlanetsCompleted = data.PlanetsCompleted;
        TEMP_ScoreForPlanet = data.ScoreForPlanet;
        TEMP_TimesLost = data.TimesLost;
        TEMP_TimesPlayed = data.TimesPlayed;
        TEMP_DevScoreBeat = data.DevScoreBeat;
    }

    public void SetPC(bool State, int index)
    {
        TEMP_PlanetsCompleted[index] = State;
    }

    public void SetPS(int Score, int index)
    {
        TEMP_ScoreForPlanet[index] = Score;
    }

    public void SetTL(int Times, int index)
    {
        TEMP_TimesLost[index] = Times;
    }

    public void SetTP(int Times, int index)
    {
        TEMP_TimesLost[index] = Times;
    }

    public void SetDB(bool State, int index)
    {
        TEMP_PlanetsCompleted[index] = State;
    }

}
