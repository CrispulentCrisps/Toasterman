using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform tf;
    public Rigidbody2D rb;

    private Vector2 Movement;

    public string BulletName;

    private float speedy;
    private float speedx;

    public float ArcSize;
    public float Offset;

    public float LevelToExplode;
    public int BulletAmount;
    [Header("True=X, False=Y")]
    public bool XorY;
    [Header("Randomness of set off range")]
    public bool RandSetOff;
    public float MinSet;
    public float MaxSet;
    public float OffsetSet;

    public void OnObjectSpawn()
    {
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        if (RandSetOff)
        {
            LevelToExplode = Random.Range(MinSet, MaxSet) + OffsetSet;
        }
        if (XorY)
        {
            speedx -= 9.81f * Time.deltaTime;
            Movement.x = speedx;
            if (tf.position.x <= LevelToExplode)
            {
                Explode(ArcSize, BulletAmount, BulletName, tf, Offset);
            }
        }
        else
        {
            speedy -= 9.81f * Time.deltaTime;
            Movement.y = speedy;
            if (tf.position.y <= LevelToExplode)
            {
                Explode(ArcSize, BulletAmount, BulletName, tf, Offset);
            }
        }
        tf.Rotate(0f,0f,5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * Time.deltaTime);
    }

    public void Explode(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset)
    {
        BulletPatternsModule.ShootArc(ArcSize,BulletAmount,BulletName,tf,Offset);
        objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
