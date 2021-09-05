using UnityEngine;

public class ControllerMenuSort : MonoBehaviour
{
    public static int PlanetIndex;
    public int MaxPlanet;
    private bool Changed = false;
    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f && !Changed)
        {
            PlanetIndex++;
            Changed = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f && !Changed)
        {
            PlanetIndex--;
            Changed = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Changed = false;
        }

        if (PlanetIndex < -1)
        {
            PlanetIndex = -1;
        }

        if (PlanetIndex > MaxPlanet)
        {
            PlanetIndex = MaxPlanet;
        }
    }
}