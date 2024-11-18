using UnityEngine;

public class CrystaliteAI : MonoBehaviour, IPooledObject
{
    protected ObjectPools objectPooler;
    EnemyScript enemyscript;
    public SpriteRenderer[] sr;
    public Color hurtColour;
    private Vector2 CPos;
    private Vector2 Movement;
    public string ExplosionName;
    public string ExplosionSound;

    public string EnemyShootSound;
    public string EnemyHurtSound;
    public float Health;

    public string BulletName;
    public int RegularAmount;

    public float RegularAngle;
    public float AngleOffset;

    public float Timer;
    public float Size = 3.0f;
    public float Angle;
    private float P;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        EnemyScript.EnemyAmount++;
        transform.position = new Vector2(enemyscript.Waves[enemyscript.i].StartYpos, 13f);
        CPos = transform.position;
        Timer = 0;
        Size = 3;
    }

    private void Update()
    {
        P += Time.deltaTime;
        if (transform.position.y >= 0.01)
        {
            transform.position += (Vector3)Movement;
            Movement.y = -0.25f;
        }
        else
        {
            Movement.y = 0;
            transform.position = new Vector2(CPos.x + Mathf.Sin(P * 0.25f), transform.position.y);
        }
        RegularAmount = 2 + (int)Mathf.Clamp(Mathf.FloorToInt(2f / Size), 0f, 2f);
        Timer += Time.deltaTime;
        if (Timer > .1f)
        {
            Timer = 0;
            Angle += 4;
            BulletPatternsModule.ShootArc(RegularAngle, RegularAmount, BulletName, transform, Angle);
        }
        transform.localScale = Vector3.one * Size;
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
            Size -= (coll.GetComponent<DamageScript>().Damage / 100.0f);

            if (Size <= 1f && gameObject.active)
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
