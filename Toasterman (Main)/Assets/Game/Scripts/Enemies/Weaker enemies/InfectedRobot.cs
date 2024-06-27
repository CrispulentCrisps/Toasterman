using UnityEngine;

public class InfectedRobot : EnemyShootScript
{
    //ObjectPools objectPooler;
    [SerializeField] private Transform CentralLazerPos;
    [SerializeField] private Transform[] LazerPos;
    [SerializeField] private string SecondExplosion;
    [SerializeField] private float rotatespeed;
    [SerializeField] private string DeathBulletname;
    [SerializeField] private int DeathBulletAmount;
    private float[] LazerAngleOff;
    private float Phase;

    public void OnObjectSpawn()
    {
        LazerAngleOff = new float[LazerPos.Length];
        for (int i = 0; i < LazerPos.Length; i++)
        {
            LazerAngleOff[i] = (360 / LazerPos.Length) * i;
        }
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        tf = transform;
        EnemyScript.EnemyAmount++;
        int I = enemyscript.i;
        speed = new Vector2(enemyscript.Waves[I].EnemySpeed * enemyscript.Waves[I].Inverse, 0);//This determines movement speed

        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
        LazerAngleOff = new float[LazerPos.Length];
        for (int i = 0; i < LazerPos.Length; i++)
        {
            LazerAngleOff[i] = (360 / LazerPos.Length) * i;
        }
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        tf = transform;
        int I = enemyscript.i;

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

        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
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
    void FixedUpdate()
    {
        Phase += Charge * Time.deltaTime;
        CentralLazerPos.Rotate(new Vector3(0, 0, rotatespeed * Time.deltaTime));
        if (Phase >= Full)
        {
            ShootBullet();
        }
        if (Health <= 0f && gameObject.active)
        {
            Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
            objectPooler.SpawnFromPool(ExplosionName, tf.position, Quaternion.identity);
            objectPooler.SpawnFromPool(SecondExplosion, tf.position, Quaternion.identity);
            if (ExplosionSound != "")
            {
                AudioManager.instance.Play(ExplosionSound);
            }
            BulletPatternsModule.ShootArc(360, DeathBulletAmount, DeathBulletname, transform, 0f);
            EnemyScript.EnemyAmount--;
            gameObject.SetActive(false);
        }
        rb.MovePosition(rb.position - speed * Time.deltaTime);
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
        }
    }


    public override void ShootBullet()
    {
        Phase = 0f;
        AudioManager.instance.ChangePitch(EnemyShootSound, Random.Range(0.5f, 1.5f));
        AudioManager.instance.Play(EnemyShootSound);
        for (int i = 0; i < LazerPos.Length; i++)
        {
            switch (ShootType)
            {
                default:
                    break;
                case 0:
                    BulletPatternsModule.ShootArc(RegularAngle, RegularAmount, BulletName, LazerPos[i], AngleOffset + CentralLazerPos.rotation.eulerAngles.z + LazerAngleOff[i]);
                    break;
                case 1:
                    BulletPatternsModule.ShootLine(BaseSpeed, MinVel, MaxVel, RegularAmount, BulletName, LazerPos[i], AngleOffset + CentralLazerPos.rotation.eulerAngles.z + LazerAngleOff[i]);
                    break;
                case 2:
                    BulletPatternsModule.ShootArcLine(BaseSpeed, MinVel, MaxVel, RegularAmount, BulletName, LazerPos[i], AngleOffset + CentralLazerPos.rotation.eulerAngles.z + LazerAngleOff[i], RegularAngle, ArcRepeat);
                    break;
                case 3:
                    BulletPatternsModule.ShootArcGap(RegularAngle, GapSize, GapOffset, RegularAmount, BulletName, LazerPos[i], AngleOffset + CentralLazerPos.rotation.eulerAngles.z + LazerAngleOff[i]);
                    break;
            }
        }
    }
}
