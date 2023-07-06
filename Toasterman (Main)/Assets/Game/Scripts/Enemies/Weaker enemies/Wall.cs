using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public EnemyScript enemyscript;
    
    public Transform tf;

    public SpriteRenderer Sr;

    private Vector2 CenterPos = new Vector2(12f,1f);

    public Color hurtColour;

    public string ExplosionName;
    public string ExplosionSound;
    public string EnemyHurtSound;
    public float Health;

    public float Speed;
    float ShakeAmount = 5f;
    float t = 0;
    bool Moving = false;
    bool Dead = false;

    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        tf.position = new Vector3(13f, -14f);
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        tf.position = new Vector3(13f, -14f);
        if (ExplosionName == "")
        {
            ExplosionName = "Boom";
        }
        if (ExplosionSound == "")
        {
            ExplosionSound = "SmallExplosion";
        }
    }

    void Update()
    {
        Sr.color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;
        if (Dead)
        {
            t += Time.deltaTime;
            tf.position -= new Vector3(ShakeAmount, 4f) * Time.deltaTime;
            ShakeAmount *= -1f;
            if (t >= 0.1f)
            {
                t = 0;
                for (int i = 0; i < 4; i++)
                {
                    objectPooler.SpawnFromPool(ExplosionName, new Vector3(Random.RandomRange(tf.position.x - 2f, tf.position.x + 2f), Random.RandomRange(tf.position.y - 6f, tf.position.y + 6f), 0f), Quaternion.identity);
                }
            }

            if (tf.position.y <= -15f)
            {
                gameObject.active = false;
            }
        }
        else
        {
            if (tf.position.y < 0f)
            {
                Moving = false;
            }
            else
            {
                Moving = true;
            }

            if (Moving)
            {
                if (Sr.flipX == false)
                {
                    tf.position -= new Vector3(Speed * Time.deltaTime, 0f);
                }
                else
                {
                    tf.position += new Vector3(Speed * Time.deltaTime, 0f);
                }
            }
            else
            {
                tf.position += new Vector3(ShakeAmount, 4f) * Time.deltaTime;
                ShakeAmount *= -1f;
            }
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
            Sr.color = hurtColour;
            if (Health <= 0f)
            {
                EnemyScript.EnemyAmount--;
                Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                Dead = true;
            }
        }
    }
}
