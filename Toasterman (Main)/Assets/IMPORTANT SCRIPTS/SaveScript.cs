using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveScript : MonoBehaviour
{
    bool[] PlanetsCompleted;

    public SaveScript (PlanetTally planetTally)
    {
        for (int i = 0; i < PlanetsCompleted.Length; i++)
        {
            planetTally.PlanetsDone[i] = PlanetsCompleted[i];
        }
    } 
}
