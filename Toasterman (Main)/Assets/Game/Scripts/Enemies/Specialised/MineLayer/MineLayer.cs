using UnityEngine;

public class MineLayer : MonoBehaviour, IPooledObject
{
    protected ObjectPools objectPooler;
    private EnemyScript enemyscript;
    private Vector2 Movement;

    [SerializeField] private Transform BodyTransform;
    [SerializeField] private float AngleSpeed;
    [SerializeField] private SpriteRenderer[] sr;
    [SerializeField] private int BulletAmount;
    [SerializeField] private string BulletName;
    [SerializeField] private float BulletAngleOff;
    [SerializeField] private string EnemyShootSound;
    [SerializeField] private string EnemyHurtSound;
    [SerializeField] private string ExplosionName;
    [SerializeField] private string ExplosionSound;
    [SerializeField] private Color hurtColour;
    [SerializeField] private float Health;
    [SerializeField] private float LayTime;
    private float Lay_t;
    private float Angleoff;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        EnemyScript.EnemyAmount++;
        if (enemyscript.Waves[enemyscript.i].XY == EnemySet.XorY.X)//Assume we are working on the X axis
        {
            Movement = new Vector2(-enemyscript.Waves[enemyscript.i].EnemySpeed * enemyscript.Waves[enemyscript.i].Inverse, 0f);
            Angleoff = 0;
        }
        else//working on Y axis
        {
            Movement = new Vector2(0f,enemyscript.Waves[enemyscript.i].EnemySpeed * enemyscript.Waves[enemyscript.i].Inverse);
            Angleoff = 90;
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
                gameObject.SetActive(false);
            }

            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = hurtColour;
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0;i < sr.Length;i++) 
        {
            sr[i].color += (Color)Vector4.one * Time.deltaTime * 5f;
        }
        transform.Translate(Movement * Time.deltaTime);
        BodyTransform.Rotate(new Vector3(0f, 0f, AngleSpeed) * Time.deltaTime);
        if (transform.position.x < -30 || transform.position.x > 30 || transform.position.y < -30 || transform.position.y > 30)
        {
            EnemyScript.EnemyAmount--;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 12 || transform.position.x > -12 || transform.position.y < 9 || transform.position.y > -9 ) Lay_t += Time.deltaTime;
     
        if (Lay_t > LayTime)
        {
            BulletPatternsModule.ShootArc(360, BulletAmount, BulletName, transform, BulletAngleOff + Angleoff);
            Lay_t = 0;
        }
    }
}
