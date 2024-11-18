using UnityEngine;

public class HomingSawAI : MonoBehaviour, IPooledObject
{
    protected EnemyScript enemyscript;
    protected ObjectPools objectPooler;

    [SerializeField] private Transform BodyTransform;
    [SerializeField] private float AngleSpeed;
    [SerializeField] private SpriteRenderer[] sr;
    [SerializeField] private Sprite[] EyeSprites;
    [SerializeField] private int EyeIndex;
    [SerializeField] private int BulletAmount;
    [SerializeField] private string BulletName;
    [SerializeField] private float BulletAngleOff;
    [SerializeField] private string EnemyShootSound;
    [SerializeField] private string EnemyHurtSound;
    [SerializeField] private string ExplosionName;
    [SerializeField] private string ExplosionSound;
    [SerializeField] private Color hurtColour;
    [SerializeField] private float Health;

    [SerializeField] protected float TimeBeforeThrow;
    [SerializeField] protected float Timer;
    [SerializeField] protected float MaxSpeed;
    [SerializeField] protected bool Fired = false;
    protected Vector2 CurrentVelocity;
    Transform TargetPoint;
    Vector3 AimPos;//For when the object is spawned
    bool StartAiming = false;

    public void OnObjectSpawn()
    {
        Fired = false;
        TargetPoint = GameObject.FindGameObjectWithTag("Player").transform;
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        EnemyScript.EnemyAmount++;
        if (enemyscript.Waves[enemyscript.i].XY == EnemySet.XorY.X)//Assume we are working on the X axis
        {
            AimPos = new Vector3(12 * enemyscript.Waves[enemyscript.i].Inverse, (enemyscript.WallSpace / enemyscript.Waves[enemyscript.i].Amount) * 12 + enemyscript.Waves[enemyscript.i].StartYpos);
            transform.position = new Vector3(AimPos.x + (2 * enemyscript.Waves[enemyscript.i].Inverse), AimPos.y);
        }
        else//working on Y axis
        {
            //AimPos = new Vector3((enemyscript.WallSpace / enemyscript.Waves[enemyscript.i].Amount) * 12 + enemyscript.Waves[enemyscript.i].StartYpos, 12 * enemyscript.Waves[enemyscript.i].Inverse);
            //transform.position = new Vector3(AimPos.x, AimPos.y - (2 * enemyscript.Waves[enemyscript.i].Inverse));
        }
        Timer = 0f;
    }
    
    void Start()
    {
        CurrentVelocity = Vector2.zero;
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        TargetPoint = GameObject.FindGameObjectWithTag("Player").transform;
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        }
        Timer += Time.deltaTime;
        if (!StartAiming)
        {
            transform.position = Vector3.Lerp(transform.position, AimPos, Timer);
            if(Vector3.Distance(transform.position, AimPos) < 0.01f)
            {
                StartAiming = true;
                Timer = 0;
            }
        }
        AngleSpeed = (Timer / TimeBeforeThrow) * 45f;
        EyeIndex = Mathf.RoundToInt((Timer / TimeBeforeThrow)*1.5f);
        if (EyeIndex < EyeSprites.Length)
        {
            sr[0].sprite = EyeSprites[EyeIndex];
        }
        if (Timer > TimeBeforeThrow && !Fired)
        {
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(TargetPoint.position.y - transform.position.y, TargetPoint.position.x - transform.position.x));
            transform.Rotate(0f, 0f, angle);
            Fired = true;
        }

        if (Fired)
        {
            CurrentVelocity += new Vector2(MaxSpeed/TimeBeforeThrow, 0f) * Time.deltaTime;
        }

        if (StartAiming)
        {
            if (transform.position.x < -13 || transform.position.x > 13 || transform.position.y < -7 || transform.position.y > 7)
            {
                BulletPatternsModule.ShootArc(360, BulletAmount, BulletName, transform, BulletAngleOff);
                gameObject.active = false;
            }
        }
    }

    void FixedUpdate()
    {
        BodyTransform.Rotate(0, 0, AngleSpeed);
        transform.Translate(CurrentVelocity * Time.deltaTime);
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
                objectPooler.SpawnFromPool(ExplosionName, transform.position, Quaternion.identity);
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

}