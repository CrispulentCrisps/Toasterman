using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public Transform tf;
    public Vector2 Movement;
    float DeezNutz; //Time variable
    float Max;
    float speed;

    void Start()
    {
        Max = Random.Range(5f, 20f);
        speed = Random.Range(-25f, -75f);
    }

    void Update()
    {
        DeezNutz += Time.deltaTime;
        if (DeezNutz > Max)
        {
            speed = Random.Range(-25f, -75f);
            tf.position = new Vector3(Random.Range(0f, 15f), 8f, 0f);
            DeezNutz = 0;
            Max = Random.Range(5f, 20f);
        }
        tf.localScale = new Vector2(-speed / 150f, -speed / 150f);
        tf.Translate(Movement * 5f * Time.deltaTime);
    }
}
