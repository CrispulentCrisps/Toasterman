using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegmentAI : MonoBehaviour
{

    public Transform tf;

    ObjectPools objectPooler;

    public WormWholeAI wormwholeai;

    public float XVel;
    private float RotSpeed;

    private BoxCollider2D boxCollider;

    private bool ShotOff = false;

    public void OnObjectSpawn()
    {

        XVel = 0f;
     
    }

    void Start()
    {

        objectPooler = ObjectPools.Instance;

        boxCollider = gameObject.GetComponent<BoxCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            wormwholeai.Health -= coll.GetComponent<DamageScript>().Damage;

        }

    }

    void Update()
    {
        if (wormwholeai.Alive == false)
        {

            

            tf.Rotate(new Vector3(0f, 0f, RotSpeed * Time.deltaTime));

        }

        if (wormwholeai.Health <= 0 && ShotOff == false)
        {
            objectPooler.SpawnFromPool("Blood", tf.position, Quaternion.identity);
            RotSpeed = Random.Range(-3600f, 3600f);
            XVel = Random.Range(-25f, 25f);
            ShotOff = true;
            boxCollider.size = new Vector2(0f, 0f);
            wormwholeai.Alive = false;
        }

        tf.position += new Vector3(XVel,0f,0f) * Time.deltaTime;

    }

}
