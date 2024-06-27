using UnityEngine;

public class BulletAI : MonoBehaviour, IPooledObject
{
    public Transform tf;

    public TrailRenderer tr;

    private GameObject MovingLevel;

    private Vector2 Movement;
    public Vector2 Bounds;

    public string BulletParticle;
    public string BulletSoundOnImpact;

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

    [Header("Rotation")]
    public bool Rotate;
    public float AngleTarget;
    public float AngleTime;
    private float timer = 0;

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

    [Header("Enclosed level shit")]
    public static Vector2 SpeedOffset;

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
        if (Bounds.x == 0 || Bounds.y == 0)
        {
            Bounds.x = 40f;
            Bounds.y = 30f;
        }

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
                            if (BulletSoundOnImpact != "")
                            {
                                AudioManager.instance.Play(BulletSoundOnImpact);
                                AudioManager.instance.ChangePitch(BulletSoundOnImpact, Random.Range(1.1f, 0.9f));
                            }
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

        //Rotation
        if (Rotate)
        {
            timer += Time.deltaTime;
            if (timer >= AngleTime)
            {
                Rotate = false;
            }

            tf.Rotate(new Vector3(0f, 0f, AngleTarget * (AngleTime * Time.deltaTime)));
            
            if (AngleTarget > 0 && tf.rotation.z > AngleTarget)
            {
                tf.rotation = Quaternion.Euler(0f, 0f, AngleTarget);
                Rotate = false;
            }
            else if (AngleTarget < 0 && tf.rotation.z < AngleTarget)
            {
                tf.rotation = Quaternion.Euler(0f, 0f, AngleTarget);
                Rotate = false;
            }
        }

        if (tf.position.x > Camera.main.transform.position.x + Bounds.x || tf.position.x < Camera.main.transform.position.x - Bounds.x || tf.position.y > Camera.main.transform.position.y + Bounds.y || tf.position.y < Camera.main.transform.position.y - Bounds.y)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        tf.Translate((Movement + SpeedOffset) * Time.deltaTime);
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
