using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    Transform PlayerTf;
    public Transform tf;
    public Transform EyeTf;
    public SpriteRenderer sr;
    float EyeWidth;
    float EyeHeight;
    float dx;
    float dy;

    private void Start()
    {
        PlayerTf = GameObject.Find("Ship").GetComponent<Transform>();
        EyeWidth = sr.bounds.size.x;
        EyeHeight = sr.bounds.size.y;
    }
    void Update()
    {
        //Thank you Blumba for helping with this code :]    
        dx = (PlayerTf.position.x - EyeTf.position.x);
        dy = (PlayerTf.position.y - EyeTf.position.y);
        float I = Mathf.Sqrt(dx * dx + dy * dy);
        dx /= I;
        dy /= I;
        dx *= (0.25f * EyeWidth);
        dy *= (0.25f * EyeHeight);
        tf.position = new Vector3(EyeTf.position.x + dx, EyeTf.position.y + dy, tf.position.z);
    }
}
