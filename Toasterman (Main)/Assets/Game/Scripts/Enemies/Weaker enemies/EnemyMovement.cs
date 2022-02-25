using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IPooledObject
{

    public Vector2 speed;
    public Rigidbody2D rb;
    public Transform tf;
    public GameObject Explosion;
    public GameObject WaveMaker;
    public EnemyScript enemyscript;

    ObjectPools objectPooler;

    public int Damage;

    private int I; // Wave number
    public bool Tri = false;
    public float YPos = 0f;
    private bool DoneUp;
    private int Delay;

    public float Health;

    public void OnObjectSpawn()
    {

        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        YPos = 0 + enemyscript.TriPos;

        objectPooler = ObjectPools.Instance;

        speed = new Vector2(enemyscript.Waves[I].EnemySpeed * enemyscript.Waves[I].Inverse, 0);
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        if (tf.position.x <= -15)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            if (Health <= 0)
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
