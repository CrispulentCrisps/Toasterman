using UnityEngine;

public class ScanlinesSettings : MonoBehaviour
{
    public SpriteRenderer sr;
    public static float Opacity = 0.25f;
    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(255f,255f,255f,Opacity);
    }
}
