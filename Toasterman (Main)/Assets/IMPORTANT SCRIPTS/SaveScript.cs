using UnityEngine;

[System.Serializable]
public class SaveScript : MonoBehaviour
{
    public bool[] PlanetsCompleted;
    public int[] ScoreForPlanet;

    public SaveScript (bool[] planetTally, int[] PlanetScore)
    {
        for (int i = 0; i < PlanetsCompleted.Length; i++)
        {
            planetTally[i] = PlanetsCompleted[i];
        }

        for (int i = 0; i < ScoreForPlanet.Length; i++)
        {
            PlanetScore[i] = ScoreForPlanet[i];
        }
    }
}
