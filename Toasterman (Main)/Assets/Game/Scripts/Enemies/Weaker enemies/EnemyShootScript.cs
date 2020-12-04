using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour, IPooledObject
{
    public Vector2 speed;

    public Rigidbody2D rb;

    public Transform tf;

    public GameObject Ship;

    public EnemyScript enemyscript;

    public Animator anim;

    public float Health;

    public string BulletName;

    private Quaternion BulletRot;

    public float RegularAngle;
    public int RegularAmount;

    private float Angle;

    ObjectPools objectPooler;


    private int I; // Wave number
    public int Charge;

    public float Full;

    private float FireRate;

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        speed = new Vector2(enemyscript.Waves[I].EnemySpeed, 0);
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void ShootBullet()
    {
        BulletPatternsModule.ShootArc(135f, 3, BulletName, tf, 135f * 0.675f);
    }

    void Update()
    {

        if (tf.position.x <= -16)
        {
            gameObject.SetActive(false);

        }else if (tf.position.x <= 14f)
        {
            FireRate += Charge * Time.deltaTime;
        }

        if (FireRate >= Full)
        {
            anim.Play("Fire");
            FireRate = 0;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;

            if (Health <= 0f)
            {
                objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        //change position
        rb.MovePosition(rb.position - speed * Time.deltaTime); //DO NOT CHANGE !!!!!!!
    }
}
