using UnityEngine;

public class PlanetTally : MonoBehaviour
{
    public bool[] PlanetsDone;
    public void LoadData()
    {
        SaveScript Data = SaveSystem.LoadData();

        for (int i = 0; i < PlanetsDone.Length; i++)
        {
            PlanetsDone[i] = Data;
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }
}
