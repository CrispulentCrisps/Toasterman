using UnityEngine;

public class PlanetSelectMouseUI : MonoBehaviour
{
    public Transform tf;
    public Transform[] PlanetPos;
    public SpriteRenderer sr;
    public RectTransform BackButton;
    public SceneChange sc;

    private Vector3 mousePosition;
    private Vector2 Target;
    
    public float speed;
    public float AttractLength;

    private float SpeedMult = 25f;

    public static bool Selected = false;
    private bool Following = true;

    void Start()
    {
        Selected = false;
        Following = true;
    }

    void Update()
    {
        tf.Rotate(new Vector3(0, 0, speed * SpeedMult));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < PlanetPos.Length; i++)
        {
            //Distance formula
            float Length = (Mathf.Pow(PlanetPos[i].position.x - mousePosition.x, 2) + Mathf.Pow(PlanetPos[i].position.y - mousePosition.y, 2));
            if (Length < AttractLength)
            {
                SpeedMult = 50f;
                Target = PlanetPos[i].position;
                i = PlanetPos.Length;//Breaks from loop as it has found a planet
            }
            else
            {
                SpeedMult = 25f;
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
        }
        if (Selected)
        {
            Following = false;
            SpeedMult += 75;
            tf.localScale = new Vector3(tf.localScale.x + 15 * Time.deltaTime,tf.localScale.y + 15 * Time.deltaTime);
            sr.color -= new Color(0f,0f,0f,1f) * Time.deltaTime;
        }
    }
}
