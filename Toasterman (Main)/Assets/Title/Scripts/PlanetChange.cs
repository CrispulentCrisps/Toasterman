using UnityEngine;

public class PlanetChange : MonoBehaviour
{
    public GameObject[] Ailments;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        if (PlanetTally.PlanetsDone[index])
        {
            for (int i = 0; i < Ailments.Length; i++)
            {
                Ailments[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Ailments.Length; i++)
            {
                Ailments[i].SetActive(true);
            }
        }
    }
}
