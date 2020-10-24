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

    public Rigidbody2D rb;

    private Vector2 Speed;

    public float RotSpeed;
    public float DragSpeed;

    public bool XORY;

    private float Health = 1f;

    public void OnObjectSpawn()
    {

        tf.position = GameObject.Find("TailShotPoint").transform.position;
        if (XORY == true)
        {

            Speed = new Vector2(Random.Range(-5f, -10f), Random.Range(-4f, 4f));

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

    // Start is called before the first frame update
    private void Start()
    {

        tf.position = GameObject.Find("TailShotPoint").transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        tf.Rotate(new Vector3(0,0,RotSpeed));

        if (Health <= 0f)
        {
            FindObjectOfType<AudioManager>().Play("TailShotHit");
            gameObject.SetActive(false);

        }

    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position - Speed * Time.deltaTime);

        Speed.x += DragSpeed;

    }
}
