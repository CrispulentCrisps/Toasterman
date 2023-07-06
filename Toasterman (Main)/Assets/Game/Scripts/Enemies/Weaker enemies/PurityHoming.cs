using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurityHoming : MonoBehaviour, IPooledObject
{
    public Transform tf;
    public GameObject Ship;
    public float speed;

    public float Health;

    ObjectPools objectPooler;

    public void OnObjectSpawn()
    {
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
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
                Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                objectPooler.SpawnFromPool("PurityDie", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(new Vector3(0, 0, speed));
        tf.position = Vector3.MoveTowards(tf.position, Ship.transform.position, speed * Time.deltaTime);
    }
}
