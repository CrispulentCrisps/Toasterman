using UnityEngine;

public class BulletAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    
    private Vector2 Movement;

    public float speedx = 11;
    public float speedy = 0;
    private float BulletRot;
    private float SineT;
    public float Amp;
    public float Freq;
    public float AmpInc;

    private int Length;

    public bool Wavy;
    public bool WavyIncAmp;
    public bool Specifics;
    public bool Killable;

    public string[] CollisionNames;

    ObjectPools objectPooler;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Specifics == true)
           {

            Length = CollisionNames.Length;

            for (int i = 0; i < Length; i++)
            {
                if (coll.gameObject.CompareTag(CollisionNames[i]))
                {

                    if (Killable == true)
                    {

                        objectPooler.SpawnFromPool("BulletHit", tf.position, Quaternion.identity);

                        gameObject.SetActive(false);

                    }

                }

            }
        }

    }

    void Update()
    {
        Movement = new Vector2(speedx, speedy);

        if (Wavy == true)
        {
            SineT += Time.deltaTime;
            speedy = Amp * Mathf.Sin(SineT * Freq);
            if (WavyIncAmp == true)
            {
                Amp += AmpInc * Time.deltaTime;
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);

        if (tf.position.x >= 18f || tf.position.x <= -18f || tf.position.y >= 10f || tf.position.y <= -10f)
        {

            gameObject.SetActive(false);

        }
    }

}
