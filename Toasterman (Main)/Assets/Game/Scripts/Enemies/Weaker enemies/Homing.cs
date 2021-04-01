using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour, IPooledObject
{

    public Transform tf;
    public GameObject Ship;
    private float speed;
    public GameObject MySelf;
    public GameObject WaveMaker;
    public EnemyScript enemyscript;
    private int I; //Wave number

    public float Health;

    ObjectPools objectPooler;

    public void OnObjectSpawn()
    {
        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        speed = enemyscript.Waves[I].EnemySpeed;
        tf.Rotate(new Vector3(0, 0, tf.position.x * 10));
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        if (tf.position.x <= -12)
        {
            tf.position = new Vector2(-999, -999);
            MySelf.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Health = 0;
        }

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

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(new Vector3(0, 0, speed));
        tf.position = Vector3.MoveTowards(tf.position,Ship.transform.position, speed * Time.deltaTime);
    }
}
