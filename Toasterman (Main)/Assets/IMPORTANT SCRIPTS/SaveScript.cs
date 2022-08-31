using UnityEngine;
using System.IO;

[System.Serializable]
public class Data 
{
    public bool[] PlanetsCompleted = { false, false };
    public float[] ScoreForPlanet = { 0, 0 };
    public int[] TimesLost = { 0, 0 };
    public int[] TimesPlayed = { 0, 0 };
    public bool[] DevScoreBeat = { false, false };
}
public class SaveScript : MonoBehaviour
{
    public bool[] TEMP_PlanetsCompleted = { false, false };
    public float[] TEMP_ScoreForPlanet = { 0, 0 };
    public int[] TEMP_TimesLost = { 0, 0 };
    public int[] TEMP_TimesPlayed = { 0, 0 };
    public bool[] TEMP_DevScoreBeat = { false, false };
    Data data;
    string path;

    void OnAwake()
    {
        path = Application.dataPath + "/SaveData/SaveData.json";
        Debug.Log(path);
        try
        {
            LoadData();
        }
        catch (System.Exception e)
        {
            Debug.Log("ERROR LOADING DATA");
            Debug.LogError(e);
            throw;
        }
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
        path = Application.dataPath + "/SaveData/SaveData.json";
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

    public void SetPS(float Score, int index)
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
