using UnityEngine;
public class SquishyAI : EnemyShootScript
{
    [SerializeField] private AnimationCurve SpeedCurve;
    [SerializeField] private float InitialSpeed;
    private float TimeEval = 0f;
    private bool AlreadyShot = false;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
        Move = true;
    }
    public override void OnObjectSpawn()
    {       
        transform.position = new Vector3((enemyscript.Waves[enemyscript.i].StartYpos + (enemyscript.WallSpace / enemyscript.Waves[enemyscript.i].Amount) * 20f) - (enemyscript.Waves[enemyscript.i].Amount*2), -9f);
        Move = true;
        EnemyScript.EnemyAmount++;
    }
    // Update is called once per frame
    void Update()
    {
        sr[0].color += new Color(6f, 6f, 6f, 6f) * Time.deltaTime;
        if (speed.y < 0.1f && !AlreadyShot)
        {
            if (tf.position.y > -8f && tf.position.y < 8f)
            {
                BulletPatternsModule.ShootArc(RegularAngle, RegularAmount, BulletName, tf, AngleOffset);
            }
            anim.Play("SquishyMove");
            TimeEval = 0f;
            AlreadyShot = true;
        }
    }

    private void FixedUpdate()
    {
        TimeEval += Time.deltaTime / 3f;
        speed.y = InitialSpeed * SpeedCurve.Evaluate(TimeEval);
        rb.MovePosition(rb.position + speed * Time.deltaTime);
        AlreadyShot = false;
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
}
