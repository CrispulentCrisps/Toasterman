using UnityEngine;

public class ScrapEvents : MonoBehaviour
{
    public GameObject[] RemovableLayers;
    public ParalaxStuff ps;
    public AnimationCurve BGCurve;
    public void StartBGChange()
    {
        StartCoroutine(ps.MoveYAToB(.3f,new float[] { -13f, -13f, -9f, -9f, -11f, -11f, -12f, -12f, -15.53f, -15.53f, -12f, -12f, -21f, -21f, -22f, -22f, -23f, -23f, -1, -1, -25.99f, -25.99f }, BGCurve));
    }
    public void ChangeLayers()
    {
        for (int i = 0; i < RemovableLayers.Length; i++)
        {
            RemovableLayers[i].SetActive(false);
        }
    }
}
