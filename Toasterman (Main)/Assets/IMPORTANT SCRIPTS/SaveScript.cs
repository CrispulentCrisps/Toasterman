using UnityEngine;

[System.Serializable]
public class SaveScript : MonoBehaviour
{
    bool[] PlanetsCompleted;

    public SaveScript (bool[] planetTally)
    {
        for (int i = 0; i < PlanetsCompleted.Length; i++)
        {
            planetTally[i] = PlanetsCompleted[i];
        }
    }
}
