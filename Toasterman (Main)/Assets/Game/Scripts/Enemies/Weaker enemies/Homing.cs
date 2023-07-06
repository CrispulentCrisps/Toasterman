using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour, IPooledObject
{
    public Transform tf;
    public GameObject Ship;
    public float speed;
    public GameObject MySelf;
    public GameObject WaveMaker;
    public EnemyScript enemyscript;

    public float Health;

    ObjectPools objectPooler;

    public void OnObjectSpawn()
    {   
        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        EnemyScript.EnemyAmount++;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.FindGameObjectWithTag("Player");
        speed = enemyscript.Waves[enemyscript.i].EnemySpeed;
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
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
                EnemyScript.EnemyAmount--;
                Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(new Vector3(0, 0, speed * 2));
        tf.position = Vector3.MoveTowards(tf.position,Ship.transform.position, speed * Time.deltaTime);
    }
}
