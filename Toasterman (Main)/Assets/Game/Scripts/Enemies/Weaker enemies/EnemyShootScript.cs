using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour, IPooledObject
{
    public Vector2 speed;

    public SpriteRenderer sr;

    public Rigidbody2D rb;
    public Transform tf;

    public Color hurtColour;

    public GameObject Ship;

    public EnemyScript enemyscript;

    public Animator anim;

    public float Health;

    public string BulletName;

    private Quaternion BulletRot;

    public float RegularAngle;
    public float AngleOffset;
    [Header("this is amount of bullets shot at a time")]
    [Range(1, 25)]
    public int RegularAmount;

    private float Angle;

    ObjectPools objectPooler;

    private int I; // Wave number
    public int Charge;

    public float Full;

    public bool RotateGun;
    [Header("this is Revolutions per second")]
    [Range(-5f,5f)]
    public float GunRotateAmount;
    private float FireRate;

    public bool ShootNearPlayer;
    [Header("this is in 1 space in unity world")]
    [Range(0.1f, 5f)]
    public float DistanceToPlayer;
    public float MinTime;//Minimum time to wat before shooting

    public bool Move;

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        speed = new Vector2(enemyscript.Waves[I].EnemySpeed, 0);
        if (sr == null)
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
        }
        if (hurtColour == new Color(0f,0f,0f,0f))
        {
            hurtColour = new Color(255f,0f,0f,255f);
        }
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void ShootBullet()
    {
        BulletPatternsModule.ShootArc(RegularAngle, RegularAmount, BulletName, tf, AngleOffset);
    }

    void Update()
    {
        sr.color += new Color(5f, 5f, 5f, 255f) * Time.deltaTime;

        if (tf.position.x <= -16)
        {
            gameObject.SetActive(false);

        }else if (tf.position.x <= 16f)
        {
            FireRate += Charge * Time.deltaTime;
        }

        if (tf.position.x >= Ship.transform.position.x - DistanceToPlayer && tf.position.x <= Ship.transform.position.x + DistanceToPlayer && FireRate >= MinTime)//If close enough to the playr on the X axis
        {
            anim.Play("Fire");
        }

        if (FireRate >= Full)
        {
            anim.Play("Fire");
            FireRate = 0;
        }

        if (RotateGun)
        {
            AngleOffset += Mathf.Round((GunRotateAmount * 360f) * Time.deltaTime);
            if (AngleOffset > 360f)
            {
                AngleOffset -= 360f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            sr.color = hurtColour;
            if (Health <= 0f)
            {
                objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    void ChangeMove()
    {
        Move = !Move;
    }

    void FixedUpdate()
    {
        //change position
        if (Move)
        {
            rb.MovePosition(rb.position - speed * Time.deltaTime); //DO NOT CHANGE !!!!!!!
        }
        
    }
}
