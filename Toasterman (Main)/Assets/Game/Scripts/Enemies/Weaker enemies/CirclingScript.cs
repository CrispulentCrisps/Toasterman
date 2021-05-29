using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingScript : MonoBehaviour, IPooledObject
{

    public Transform tf;
    public Transform Target;

    public GameObject Ship;
    public GameObject WaveMaker;
    public EnemyScript enemyscript;

    public string BulletName;

    ObjectPools objectPooler;

    private int I; // Wave number
    public int BulletAmount;

    public float angle;
    public float ShotTime;
    public float ShotInterval;
    public float Radius;
    public float RotateSpeed;
    private float Timer;
    public float Health;

    private Quaternion BulletRot;

    public void OnObjectSpawn()
    {

        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;

        Ship = GameObject.Find("Ship");//gets the game object
        Target = Ship.GetComponent<Transform>();// gets the Transform 

        //https://answers.unity.com/questions/1068513/place-8-objects-around-a-target-gameobject.html thank you ^-^
        angle = enemyscript.RotSpace * Mathf.PI * 2f / enemyscript.Waves[I].Amount;

        RotateSpeed = enemyscript.Waves[I].RotateSpeed;
        Radius = enemyscript.Waves[I].Radius;

    }

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;

        }

        if (Health <= 0)
        {

            objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
            gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {

        angle += RotateSpeed * Time.deltaTime;

        Timer += Time.deltaTime;

        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        tf.position = (Vector2)Target.position + offset;

        Vector3 difference = Target.position - tf.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (Timer >= ShotTime)
        {
            StartCoroutine(BulletPatternsModule.ShootArcEnum(0f, BulletAmount, BulletName, tf, rotationZ, ShotInterval));//If arc size > 0 then the angle of the bullets breaks
            Timer = 0f;
        }
    }
}
