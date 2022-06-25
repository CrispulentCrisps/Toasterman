using UnityEngine;

public class PlanetTally : MonoBehaviour
{
    public static bool[] PlanetsDone = { false, false};
    public static int[] PlanetScore = { 0, 0 };

    public SaveScript save;

    void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        save.LoadData();
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetsDone[i] = save.TEMP_PlanetsCompleted[i];
        }
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetScore[i] = save.TEMP_ScoreForPlanet[i];
        }
    }
    public void SaveData()
    {
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            save.SetPS(PlanetScore[i], i);
            save.SetPC(PlanetsDone[i], i);
        }
        save.SaveData();
    }
}
