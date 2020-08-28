using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleHoming : MonoBehaviour, IPooledObject
{

    public Transform Target;
    public Transform tf;
    public Rigidbody2D rb;

    ObjectPools objectPooler;

    public float Health;

    public float speed;
    private float Rotspeed;
    public float RotVel;
    public float RotMaxSpeed;
    public float StartRot;
    private float timer;
    public float MaxTime;

    private void Start()
    {

        objectPooler = ObjectPools.Instance;
        tf.Rotate(0,0,StartRot);
        Target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public void OnObjectSpawn()
    {

        tf.Rotate(0, 0, StartRot);
        StartRot = 90;
        Target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {

            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;

        }else if (coll.gameObject.CompareTag("Player"))
        {

            objectPooler.SpawnFromPool("BigExplosion", tf.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().ChangePitch("Explosion", Random.Range(.1f, .75f));
            FindObjectOfType<AudioManager>().Play("Explosion");
            gameObject.SetActive(false);
            timer = 0;
            Rotspeed = 0;
            Health = 10f;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer >= 1f)
        {



        }
        Vector2 Direction = (Vector2)Target.position - rb.position;

        Direction.Normalize();

        float RotateAmount = Vector3.Cross(Direction, transform.up).z;

        if (Rotspeed <= RotMaxSpeed)
        {

            Rotspeed += RotVel;

        }

        rb.angularVelocity = -RotateAmount * Rotspeed;

        rb.velocity = transform.up * speed;

        timer += 1 * Time.deltaTime;

        if (timer >= MaxTime || Health <= 0f)
        {

            objectPooler.SpawnFromPool("BigExplosion", tf.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().ChangePitch("Explosion", Random.Range(.1f, 1f));
            FindObjectOfType<AudioManager>().Play("Explosion");
            gameObject.SetActive(false);
            timer = 0;
            Rotspeed = 0;
            Health = 10f;

        }

    }
}
