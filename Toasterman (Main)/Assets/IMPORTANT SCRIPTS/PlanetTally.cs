using UnityEngine;

public class PlanetTally : MonoBehaviour
{
    public static bool[] PlanetsDone = { false, false};
    public static float[] PlanetScore = { 0, 0 };
    public static int[] TimesCompleted = { 0, 0 };

    public SaveScript save;
    void Start()
    {
        save = gameObject.GetComponent<SaveScript>();
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
        for (int i = 0; i < TimesCompleted.Length; i++)
        {
            TimesCompleted[i] = save.TEMP_TimesPlayed[i];
        }
    }
    public void SaveData()
    {
        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            save.SetPS(PlanetScore[i], i);
            save.SetPC(PlanetsDone[i], i);
            save.SetTP(TimesCompleted[i], i);
        }
        save.SaveData();
    }
}
