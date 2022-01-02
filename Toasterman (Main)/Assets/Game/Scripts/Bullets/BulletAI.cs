using UnityEngine;

public class BulletAI : MonoBehaviour, IPooledObject
{
    public Transform tf;

    public TrailRenderer tr;

    private Vector2 Movement;

    public string BulletParticle;

    public float speedx;
    public float speedy;

    [Header("subtractive speed")]
    public bool ChangeAcc;
    public float AccX;
    public float AccY;
    public float AccMinX;
    public float AccMinY;

    [Header("sine wave movement")]
    public bool SineMove;
    public float SineAmp;
    public float SineFreq;

    [Header("Exploding into bullets")]
    public bool Explode;
    public string BulletShootName;
    public float LifetimeSave;
    private float Lifetime;
    public int BulletAmount;
    public int BulletShootType;

    [Header("Shoot type 0 or explode")]
    public float ArcSize;
    public float ArcOffset;

    [Header("Shoot type 1")]
    public bool ShootAtPlayer;
    public float BaseSpeed;
    public float MinSpeed;
    public float MaxSpeed;

    [Header("Set up stuff")]
    public int BulletHealth;
    public float speedxMem;
    public float speedyMem;
    private float ST;

    private int Length;

    public bool Specifics;
    public bool Killable;
    public bool SpeedChanged;

    public string[] CollisionNames;

    ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        if (tr != null)
        {
            tr.Clear();
        }
        if (DEBUG.ChangeGraphics == true)//CHanges sprite to toast
        {
            SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
            rend.sprite = Resources.Load<Sprite>("Toast");
        }

        if (!SpeedChanged)
        {
            speedx = speedxMem;
            speedy = speedyMem;
        }
        Movement = new Vector2(speedx, speedy);
        ST = 0f;//Sine phase

        Lifetime = LifetimeSave;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Specifics == true)
        {
            Length = CollisionNames.Length;
            for (int i = 0; i < Length; i++)
            {
                if (coll.gameObject.CompareTag(CollisionNames[i]) && Killable)
                {
                    if (BulletHealth <= 0)
                    {
                        if (BulletParticle != "")//just incase it's null
                        {
                            objectPooler.SpawnFromPool(BulletParticle, tf.position, Quaternion.identity);
                        }
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void Update()
    {
        Movement = new Vector2(speedx, speedy);
        //explode into multiple bullets
        if (Explode)
        {
            Lifetime -= Time.deltaTime;
            if (Lifetime <= 0f)
            {
                switch (BulletShootType)
                {
                    case 0:
                        BulletPatternsModule.ShootArc(ArcSize, BulletAmount, BulletShootName, tf, ArcOffset);
                        break;
                    case 1:
                        if (ShootAtPlayer)
                        {
                            Transform Target = GameObject.Find("Ship").GetComponent<Transform>();
                            Vector3 difference = Target.position - tf.position;
                            float AngleOffset = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                            BulletPatternsModule.ShootLine(BaseSpeed, MinSpeed, MaxSpeed, BulletAmount, BulletShootName, tf, AngleOffset);
                        }
                        else
                        {
                            BulletPatternsModule.ShootLine(BaseSpeed, MinSpeed, MaxSpeed, BulletAmount, BulletShootName, tf, tf.rotation.z);
                        }
                        break;
                }
                //Kill
                if (BulletParticle != "")//just incase it's null
                {
                    objectPooler.SpawnFromPool(BulletParticle, tf.position, Quaternion.identity);
                }
                gameObject.SetActive(false);
            }
        }

        //Change the speed
        if (ChangeAcc)
        {
            speedx -= AccX * Time.deltaTime;
            speedy -= AccY * Time.deltaTime;
            if (speedx <= -AccMinX || speedx >= AccMinX || speedy <= -AccMinY || speedy >= AccMinY)
            {
                if (BulletParticle != null)
                {
                    objectPooler.SpawnFromPool(BulletParticle, tf.position, Quaternion.identity);
                }
                gameObject.SetActive(false);
            }
        }

        //Sine wave movement
        if (SineMove)
        {
            ST += Time.deltaTime;
            speedy = SineAmp * Mathf.Sin(ST * SineFreq);
        }

        if (tf.position.x > 25 || tf.position.x < -25f || tf.position.y > 15f || tf.position.x < -15f)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

    public void SetSpeed(float XSpeed, float YSpeed)
    {
        if (SpeedChanged)
        {
            Vector2 TotalSpeed = new Vector2(XSpeed, YSpeed);
            Movement = TotalSpeed;
        }else
        {
            speedxMem = speedx;
            speedyMem = speedy;
            Movement = new Vector2(speedxMem, speedyMem);
        }
    }
}
