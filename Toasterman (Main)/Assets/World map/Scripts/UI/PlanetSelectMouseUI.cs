using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectMouseUI : MonoBehaviour
{
    public Transform tf;
    public Transform[] PlanetPos;
    public SpriteRenderer sr;

    private Vector3 mousePosition;
    private Vector2 Target;
    
    public float speed;
    public float AttractLength;
    public float ResetSize;
    public float EnlargeSize;

    private float SpeedMult = 25f;

    public static bool Selected = false;
    private bool Following = true;
    private bool OverPlanet = false;

    void Update()
    {
        tf.Rotate(new Vector3(0, 0, speed * SpeedMult));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < PlanetPos.Length; i++)
        {
            float Length = (Mathf.Pow(PlanetPos[i].position.x - mousePosition.x, 2) + Mathf.Pow(PlanetPos[i].position.y - mousePosition.y, 2));
            if (Length < AttractLength)
            {
                SpeedMult = 50f;
                OverPlanet = true;
                Target = PlanetPos[i].position;
                i = PlanetPos.Length;//Breaks from loop as it has found a planet
            }
            else
            {
                SpeedMult = 25f;
                OverPlanet = false;
                Target = mousePosition;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Following)
        {
            tf.position = Vector2.Lerp(tf.position, Target, speed);
            if (OverPlanet)
            {
                tf.localScale = new Vector3(Mathf.Lerp(tf.localScale.x, EnlargeSize, 0.1f), Mathf.Lerp(tf.localScale.y, EnlargeSize, 0.1f), 1f);
            }
            else
            {
                tf.localScale = new Vector3(Mathf.Lerp(tf.localScale.x, ResetSize, 0.1f), Mathf.Lerp(tf.localScale.y, ResetSize, 0.1f), 1f);
            }
        }
        if (Selected)
        {
            Following = false;
            SpeedMult += 75;
            tf.localScale = new Vector3(tf.localScale.x + 5 * Time.deltaTime,tf.localScale.y + 5 * Time.deltaTime);
            sr.color -= new Color(0f,0f,0f,1f) * Time.deltaTime;
        }
    }
}
