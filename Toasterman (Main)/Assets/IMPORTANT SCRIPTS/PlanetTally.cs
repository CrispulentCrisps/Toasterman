using UnityEngine;

public class PlanetTally : MonoBehaviour
{
    public static bool[] PlanetsDone;
    private void Start()
    {
        PlanetsDone = new bool[] { false, false };
        LoadData();
    }

    public void LoadData()
    {
        SaveScript Data = SaveSystem.LoadData();

        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetsDone[i] = Data;
        }
    }
    public static void SaveData()
    {
        SaveSystem.SaveData(PlanetsDone);
    }
}
