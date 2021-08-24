using UnityEngine;

//Actual enemy stuff
public class EnemyShootScript : MonoBehaviour, IPooledObject
{
    public Vector2 speed;

    public SpriteRenderer sr;

    public Rigidbody2D rb;
    public Transform tf;
    [Header("Only use if ShootAtPlayer is used")]
    public Transform Target;

    public Color hurtColour;

    public GameObject Ship;

    public EnemyScript enemyscript;

    public Animator anim;
    public string EnemyShootSound;
    public string EnemyHurtSound;
    public float Health;

    public string BulletName;

    public float RotationSpeed;

    public int RegularAmount;

    ObjectPools objectPooler;

    private int I; // Wave number
    public int Charge;

    public float Full;

    public int ShootType;
    public float RegularAngle;
    public float AngleOffset;
    public float BaseSpeed;
    public float MinVel;
    public float MaxVel;
    public int ArcRepeat;

    public bool RotateGun;

    [Range(-5f,5f)]
    public float GunRotateAmount;
    private float FireRate;

    public bool ShootNearPlayer;

    public float DistanceToPlayer;
    public float MinTime;//Minimum time to wait before shooting

    public bool ShootAtPlayer;

    public bool Move;

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        Target = Ship.GetComponent<Transform>();
        speed = new Vector2(enemyscript.Waves[I].EnemySpeed, 0);//This determines movement speed

        if (sr == null)
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
        }

        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        if (hurtColour == new Color(0f,0f,0f,0f))
        {
            hurtColour = new Color(255f,0f,0f,255f);
        }

        if (EnemyShootSound == "")
        {
            EnemyShootSound = "EnemyShoot";
        }
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        Target = Ship.GetComponent<Transform>();
    }

    public void ShootBullet()
    {
        AudioManager.instance.ChangePitch(EnemyShootSound, Random.Range(0.5f,1.5f));
        AudioManager.instance.Play(EnemyShootSound);

        if (ShootAtPlayer)
        {
            Vector3 difference = Target.position - tf.position;
            AngleOffset = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - (0.5f * RegularAngle);
        }
        switch (ShootType)
        {
            default:
                break;
            case 0:
                BulletPatternsModule.ShootArc(RegularAngle, RegularAmount, BulletName, tf, AngleOffset);
                break;
            case 1:
                BulletPatternsModule.ShootLine(BaseSpeed, MinVel, MaxVel, RegularAmount, BulletName, tf, AngleOffset);
                break;
            case 2:
                BulletPatternsModule.ShootArcLine(BaseSpeed, MinVel, MaxVel, RegularAmount, BulletName, tf, AngleOffset, RegularAngle, ArcRepeat);
                break;
        }
    }

    void Update()
    {
        sr.color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        tf.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);
        if (tf.position.x <= -16)
        {
            gameObject.SetActive(false);

        }else if (tf.position.x <= 16f)
        {
            FireRate += Charge * Time.deltaTime;
        }

        if (tf.position.x >= Ship.transform.position.x - DistanceToPlayer && tf.position.x <= Ship.transform.position.x + DistanceToPlayer && FireRate >= MinTime)//If close enough to the player on the X axis
        {
            anim.Play("Fire");
        }

        if (FireRate >= Full)
        {
            anim.Play("Fire");
            FireRate = 0;
        }

        if (RotateGun)
        {
            AngleOffset += (GunRotateAmount * 360f) * Time.deltaTime;
            if (AngleOffset > 360f)
            {
                AngleOffset -= 360f;
            }
            else if (AngleOffset < -360f)
            {
                AngleOffset += 360f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            sr.color = hurtColour;
            if (Health <= 0f)
            {
                objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    void ChangeMove()//Used in animation
    {
        Move = !Move;
    }

    void FixedUpdate()
    {
        //change position
        if (Move)
        {
            rb.MovePosition(rb.position - speed * Time.deltaTime); //DO NOT CHANGE !!!!!!!
        }
        
    }
}