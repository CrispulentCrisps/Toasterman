using UnityEngine;

//Actual enemy stuff
public class EnemyShootScript : MonoBehaviour, IPooledObject
{

    protected ObjectPools objectPooler;

    public Vector2 speed;

    public SpriteRenderer[] sr;

    public Rigidbody2D rb;
    public Transform tf;

    public string ExplosionName;
    public string ExplosionSound;

    [Header("Only use if ShootAtPlayer is used")]
    public Transform Target;

    public Color hurtColour;

    public EnemyScript enemyscript;

    public Animator anim;
    public string EnemyShootSound;
    public string EnemyHurtSound;
    public float Health;

    public string BulletName;

    public int RegularAmount;

    private int I; // Wave number
    public int Charge;

    public float Full;
    [Range(0, 3)]
    public int ShootType;

    public float RegularAngle;
    public float AngleOffset;
    public float GapSize;
    public float GapOffset;
    public float BaseSpeed;
    public float MinVel;
    public float MaxVel;
    public int ArcRepeat;

    public bool RotateGun;

    [Range(-5f,5f)]
    public float GunRotateAmount;
    protected float FireRate;

    public bool ShootNearPlayer;

    public float DistanceToPlayer;
    public float MinTime;//Minimum time to wait before shooting

    public bool ShootAtPlayer;

    public bool Move;

    public virtual void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        tf = transform;
        EnemyScript.EnemyAmount++;
        I = enemyscript.i;

        Target = GameObject.FindGameObjectWithTag("Player").transform;

        if (sr == null)
        {
            sr = new SpriteRenderer[0];
            sr[0] = gameObject.GetComponent<SpriteRenderer>();
        }
        
        if (!sr[0])
        {
            sr[0] = gameObject.GetComponent<SpriteRenderer>();
        }

        if (enemyscript.Waves[I].Inverse == -1)
        {
            sr[0].flipX = true;
        }
        if (enemyscript.Waves[I].XY == EnemySet.XorY.X)
        {
            speed = new Vector2(enemyscript.Waves[I].EnemySpeed * enemyscript.Waves[I].Inverse, 0);//This determines movement speed
        }
        else
        {
            speed = new Vector2(0, enemyscript.Waves[I].EnemySpeed * enemyscript.Waves[I].Inverse);//This determines movement speed
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
        FireRate = 0;
    }

    void Awake()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        objectPooler = ObjectPools.Instance;
        if (ExplosionName == "")
        {
            ExplosionName = "Boom";
        }
        if (ExplosionSound == "")
        {
            ExplosionSound = "SmallExplosion";
        }
    }

    public virtual void ShootBullet()
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
            case 3:
                BulletPatternsModule.ShootArcGap(RegularAngle, GapSize, GapOffset, RegularAmount ,BulletName, tf, AngleOffset);
                break;
        }
    }

    void Update()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        }

        if (tf.position.x <= -30 || tf.position.x >= 30)
        {
            EnemyScript.EnemyAmount--;
            gameObject.SetActive(false);

        }else if (tf.position.x <= 16f && tf.position.x >= -16f)
        {
            FireRate += Charge * Time.deltaTime;
        }

        if (tf.position.x >= Target.position.x - DistanceToPlayer && tf.position.x <= Target.position.x + DistanceToPlayer && FireRate >= MinTime)//If close enough to the player on the X axis
        {
            anim.Play("Fire");
            FireRate = 0;
        }
        else if (FireRate >= Full)
        {
            anim.Play("Fire");
            FireRate = 0;
        }

        if (RotateGun)
        {
            AngleOffset += (GunRotateAmount * 360f) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            if (EnemyHurtSound != "")
            {
                AudioManager.instance.ChangePitch(EnemyHurtSound, Random.Range(0.75f, 1.25f));
                AudioManager.instance.Play(EnemyHurtSound);
            }
            Health -= coll.GetComponent<DamageScript>().Damage;

            if (Health <= 0f && gameObject.active)
            {
                Shooting.TargetScore += GetComponent<DamageScript>().Points * GetComponent<DamageScript>().PointMultiplier;
                objectPooler.SpawnFromPool(ExplosionName, tf.position, Quaternion.identity);
                if (ExplosionSound != "")
                {
                    AudioManager.instance.Play(ExplosionSound);
                }
                EnemyScript.EnemyAmount--;
                gameObject.SetActive(false);
            }

            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = hurtColour;
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