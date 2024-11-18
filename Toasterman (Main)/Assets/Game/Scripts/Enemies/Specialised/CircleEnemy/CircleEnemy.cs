using UnityEngine;

public class CircleEnemy : MonoBehaviour, IPooledObject
{
    protected EnemyScript enemyscript;
    protected ObjectPools objectPooler;

    [SerializeField] private Transform[] CirclingSpheres;
    [SerializeField] private float AngleSpeed;
    [SerializeField] private SpriteRenderer[] sr;
    [SerializeField] private Sprite[] AnimFrames;
    [SerializeField] private int BulletAmount;
    [SerializeField] private string BulletName;
    [SerializeField] private float BulletAngleOff;
    [SerializeField] private string EnemyShootSound;
    [SerializeField] private string EnemyHurtSound;
    [SerializeField] private string ExplosionName;
    [SerializeField] private string ExplosionSound;
    [SerializeField] private Color hurtColour;
    [SerializeField] private float Health;

    private Vector2 Movement;
    private float STime = 0f;
    private float Rad = 3f;
    private float AimRad = 0f;
    private int Frame;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        Rad = enemyscript.Waves[enemyscript.i].Radius;
        if (enemyscript.Waves[enemyscript.i].XY == EnemySet.XorY.X)
        {
            Movement = new Vector2(enemyscript.Waves[enemyscript.i].EnemySpeed * -enemyscript.Waves[enemyscript.i].Inverse, 0f);
        }
        else
        {
            Movement = new Vector2(0f, enemyscript.Waves[enemyscript.i].EnemySpeed * -enemyscript.Waves[enemyscript.i].Inverse);
        }
        STime = transform.position.x / 8;
        if (STime < 0f) STime *= -1f;
        AimRad = 0f;
        sr[0].flipX = Movement.x < 0;
        Frame = 0;
        sr[0].sprite = AnimFrames[0];
    }

    void FixedUpdate()
    {
        Frame = Mathf.RoundToInt(STime%1.5f);
        sr[0].sprite = AnimFrames[Frame];
        if (AimRad < Rad)
        {
            AimRad += Time.deltaTime * Rad;
        }
        transform.Translate(Movement * Time.deltaTime);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        }
        float off = 270f / CirclingSpheres.Length;
        STime += Time.deltaTime * AngleSpeed;
        for (int i = 0; i < CirclingSpheres.Length; i++)
        {
            CirclingSpheres[i].transform.position = transform.position + new Vector3(AimRad * Mathf.Sin(STime + (off * i)), AimRad * Mathf.Cos(STime + (off * i)), 0f);
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
                objectPooler.SpawnFromPool(ExplosionName, transform.position, Quaternion.identity);
                if (ExplosionSound != "")
                {
                    AudioManager.instance.Play(ExplosionSound);
                }
                EnemyScript.EnemyAmount--;
                BulletPatternsModule.ShootArc(360f, BulletAmount, BulletName, transform, BulletAngleOff);
                gameObject.SetActive(false);
            }

            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = hurtColour;
            }
        }
    }
}
