using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyGunAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public GameObject pointer;

    public Transform tf;
    Transform Target;

    public float TimeToShoot;
    public float Health;

    bool IsDead = false;
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        tf.position = new Vector3(20f,15f,0f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            if (Health <= 0f)
            {
                if (!IsDead)
                {
                    Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                }
                IsDead = true;
            }
        }
    }

    void Update()
    {
        TimeToShoot -= Time.deltaTime;
        if (TimeToShoot <= 0f)
        {
            Fire();
        }
    }

    public void Fire()
    {

    }
}
