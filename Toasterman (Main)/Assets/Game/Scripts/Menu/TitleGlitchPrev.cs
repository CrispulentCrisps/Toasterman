using UnityEngine;

public class TitleGlitchPrev : MonoBehaviour
{
    public GameObject ThingToActive;

    public void ChangeTitleActive()
    {
        ThingToActive.SetActive(!ThingToActive.active);
    }
}
