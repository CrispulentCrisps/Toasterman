using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class TailShotAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;
    private Transform StartPoint;

    private Vector2 Speed;

    public float DragSpeed;

    public bool XORY;

    private float Health = 1f;

    public void OnObjectSpawn()
    {
        tf.position = GameObject.Find("TailShotPoint").transform.position;
        if (XORY == true)
        {
            Speed = new Vector2(Random.Range(-5f, -10f), Random.Range(-6f, 6f));
        }
        else
        {
            Speed = new Vector2(Random.Range(-6f, 6f), Random.Range(-5f, -10f));
        }
        Health = 1f;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0f || tf.position.x <= -18f)
        {
            AudioManager.instance.Play("TailShotHit");
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        tf.Translate(-Speed * Time.deltaTime);
        Speed.x += DragSpeed;
    }
}
