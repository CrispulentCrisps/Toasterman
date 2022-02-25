using UnityEngine;

public class PlanetTally : MonoBehaviour
{
    public static bool[] PlanetsDone;
    public static int[] PlanetScore;
    private void Start()
    {
        PlanetsDone = new bool[] { false, false };
        PlanetScore = new int[] { 0, 0 };
        LoadData();
    }

    public void LoadData()
    {
        SaveScript Data = SaveSystem.LoadData();
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetsDone[i] = Data.PlanetsCompleted[i];
        }
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetScore[i] = Data.ScoreForPlanet[i];
        }
    }
    public static void SaveData()
    {
        SaveSystem.SaveData(PlanetsDone, PlanetScore);
    }
}
