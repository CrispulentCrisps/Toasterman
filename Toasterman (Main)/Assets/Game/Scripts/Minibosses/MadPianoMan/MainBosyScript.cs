using UnityEngine;

public class MainBosyScript : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public GameObject MainBod;
    public GameObject DeathFlames;
    public GameObject WaveMaker;

    public Transform[] tfs; //First 2 are for the hands themselves and the next one is for the main transform of the body

    public EnemyScript enemyscript;

    private Vector3 Velocity = Vector3.zero;

    public float TimeToDest;
    public float Speed;
    public float SAmp;//Sine Amplitude
    public float SFreq;//Sine Freq
    public float Health;

    private float ST;//Sine time
    private float SHT;//Shoot time
    private float YVel;
    private float MaxHealth;
    
    private int Max;//Maximum time until shooting

    public bool Alive;
    public bool Intro;

    // Start is called before the first frame update
    void Start()
    {
        ST = 5f;
        SHT = 0f;
        YVel = 0f;
        objectPooler = ObjectPools.Instance;
        Alive = true;
        Intro = true;
        tfs[3].position = new Vector3(0f, -25f, 0f);
        tfs[5].position = new Vector3(0f, -25f, 0f);
        Max = Random.Range(1, 5);
        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        MaxHealth = Health;
    }

    public void OnObjectSpawn()
    {
        ST = 5f;
        SHT = 0f;
        YVel = 0f;
        Alive = true;
        Intro = true;
        tfs[3].position = new Vector3(0f, -25f, 0f);
        tfs[5].position = new Vector3(0f, -25f, 0f);
        Max = Random.Range(1, 5);
        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        enemyscript.start = false;
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Intro)
        {
            ST += Time.deltaTime;
            SHT += Time.deltaTime;
            SAmp = Mathf.PingPong(ST * Speed * 0.25f, 20f) - 10f;
            if (SHT >= Max)
            {
                for (int i = 0; i < 2; i++)
                {
                    ShootCircle(8, "SmallRock", tfs[i], i * 22f);
                    FindObjectOfType<AudioManager>().Play("Tail");
                }
                SHT = 0f;
                if (Health >= MaxHealth * 0.5f)
                {
                    Max = Random.Range(1, 5);
                }
                else if (Health < MaxHealth * 0.5f)
                {
                    Max = Random.Range(0, 2);
                }
            }
            if (Health <= 0f && Alive == true)
            {
                YVel = -9.81f * 4f;
                DeathFlames.SetActive(true);
                objectPooler.SpawnFromPool("FlashBang", tfs[3].position, Quaternion.identity);
                Alive = false;
            }
        }
        else if (Intro)
        {
            enemyscript.start = false;
            tfs[5].position = new Vector3 (Mathf.PingPong(ST * Speed, 25f) - 12.5f, 5 * Mathf.Cos(ST * 0.25f), 0f);
            tfs[2].position = Vector3.SmoothDamp(tfs[2].position, tfs[5].position, ref Velocity, 1f);
            if (tfs[2].position.x <= tfs[5].position.x + 0.05f && tfs[2].position.x >= tfs[5].position.x - 0.05f)
            {
                Intro = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage * 0.5f;
            FindObjectOfType<AudioManager>().Play("WoodBreak");
            FindObjectOfType<AudioManager>().ChangePitch("WoodBreak",Random.Range(0.9f,1.1f));
        }

    }

    void LateUpdate()
    {
        if (Alive && !Intro)
        {
            if (Health >= MaxHealth * 0.5f)
            {
                tfs[3].position = new Vector3(tfs[2].position.x - 5f + SAmp * Mathf.Sin(ST * SFreq), tfs[2].position.y + SAmp * Mathf.Cos(ST * SFreq), tfs[3].position.z);
                tfs[4].position = new Vector3(tfs[2].position.x - 3f + SAmp * Mathf.Sin(ST * SFreq) * -1f, tfs[2].position.y + SAmp * Mathf.Cos(ST * SFreq) * -1f, tfs[4].position.z);
            }
            else
            {
                tfs[3].position = new Vector3(0f + 1.5f * SAmp * Mathf.Sin(ST * SFreq), 0f + 2.5f * SAmp * Mathf.Cos(ST * SFreq), tfs[3].position.z);
                tfs[4].position = new Vector3(0f + 1.5f * SAmp * Mathf.Sin(ST * SFreq) * -1f, 0f + 2.5f * SAmp * Mathf.Cos(ST * SFreq) * -1f, tfs[4].position.z);
            }
            for (int i = 0; i < 2; i++)
            {
                tfs[i].position = Vector3.SmoothDamp(tfs[i].position, tfs[i + 3].position, ref Velocity, TimeToDest);
            }
            tfs[5].position = new Vector3(Mathf.PingPong(ST * Speed, 25f) - 12.5f, 5 * Mathf.Cos(ST * 0.25f), 0f);
            tfs[2].position = Vector3.SmoothDamp(tfs[2].position, tfs[5].position, ref Velocity, TimeToDest);
            enemyscript.start = false;
        }
        else if (!Alive)
        {
            for (int i = 0; i < tfs.Length; i++)
            {
                YVel += 9.81f * Time.deltaTime;
                tfs[i].position -= new Vector3(0f, YVel, 0f) * Time.deltaTime;
                if (tfs[i].position.y <= -25f)
                {
                    enemyscript.start = true;
                    MainBod.SetActive(false);
                }
            }
        }
    }

    public void ShootCircle(int BulletAmount, string BulletName, Transform tf, float Offset)
    {
        float angle = Offset;
        for (int i = 0; i < BulletAmount; i++)
        {
            float AngleStep = 360f / BulletAmount;
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
        }

    }

}
