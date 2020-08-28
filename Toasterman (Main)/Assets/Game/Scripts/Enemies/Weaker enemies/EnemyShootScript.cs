using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour, IPooledObject
{
    public Vector2 speed;

    public Rigidbody2D rb;

    public Transform tf;

    public GameObject Ship;
    public GameObject Explosion;
    public GameObject WaveMaker;

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

    public bool Stopped = false;

    public void OnObjectSpawn()
    {

        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
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
        Angle = (-RegularAngle / 2) + 135;
        for (int i = 0; i < RegularAmount; i++)
        {

            Angle -= RegularAngle / RegularAmount;
            BulletRot = Quaternion.Euler(0, 0, Angle % 360);
            objectPooler.SpawnFromPool(BulletName, tf.position, BulletRot);

        }

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

    public void Stop()
    {

        Stopped = true;

    }

    public void Starting()
    {

        Stopped = false;

    }


    void FixedUpdate()
    {

        //change position

        if (Stopped == false)
        {

            rb.MovePosition(rb.position - speed * Time.deltaTime); //DO NOT CHANGE !!!!!!!

        }

    }
}
