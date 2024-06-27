using UnityEngine;

public class LevelEnemy : MonoBehaviour, IPooledObject
{
    public SpriteRenderer[] sr;

    public Transform tf;

    public string ExplosionName;
    public string ExplosionSound;

    [Header("Only use if ShootAtPlayer is used")]
    public Transform Target;

    public Color hurtColour;

    public Animator anim;
    public string EnemyShootSound;
    public string EnemyHurtSound;
    public float Health;

    public string BulletName;

    public float RotationSpeed;

    public int RegularAmount;

    ObjectPools objectPooler;

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

    [Range(-5f, 5f)]
    public float GunRotateAmount;
    private float FireRate;

    public bool ShootNearPlayer;

    public float DistanceToPlayer;
    public float MinTime;//Minimum time to wait before shooting

    public bool ShootAtPlayer;

    public void OnObjectSpawn()
    {

    }

    void Awake()
    {
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
        tf = transform;

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

        if (hurtColour == new Color(0f, 0f, 0f, 0f))
        {
            hurtColour = new Color(255f, 0f, 0f, 255f);
        }

        if (EnemyShootSound == "")
        {
            EnemyShootSound = "EnemyShoot";
        }
    }

    public void ShootBullet()
    {
        AudioManager.instance.ChangePitch(EnemyShootSound, Random.Range(0.5f, 1.5f));
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
                BulletPatternsModule.ShootArcGap(RegularAngle, GapSize, GapOffset, RegularAmount, BulletName, tf, AngleOffset);
                break;
        }
    }

    void Update()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        }
        tf.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);

        if (tf.position.x <= 16f && tf.position.x >= -16f)
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
            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = hurtColour;
            }
            if (Health <= 0f)
            {
                Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                objectPooler.SpawnFromPool(ExplosionName, tf.position, Quaternion.identity);
                if (ExplosionSound != "")
                {
                    AudioManager.instance.Play(ExplosionSound);
                }
                gameObject.SetActive(false);
            }
        }
    }
}