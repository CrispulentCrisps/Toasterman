using UnityEngine;

public class ScrapEvents : MonoBehaviour
{
    public GameObject[] RemovableLayers;
    public SpriteRenderer[] CLSR;
    public Sprite[] CLS;

    public void ChangeLayers()
    {
        for (int i = 0; i < CLSR.Length; i++)
        {
            CLSR[i].sprite = CLS[i];
        }
        for (int i = 0; i < RemovableLayers.Length; i++)
        {
            RemovableLayers[i].SetActive(false);
        }
    }
}
