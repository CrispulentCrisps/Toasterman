using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegmentAI : MonoBehaviour
{

    public Transform tf;

    ObjectPools objectPooler;

    public WormWholeAI wormwholeai;

    public SpriteRenderer sr;

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
            sr.color = new Color(255f,0f,0f,255f);
            wormwholeai.Health -= coll.GetComponent<DamageScript>().Damage;
        }
    }

    void Update()
    {
        sr.color += new Color(1f,1f,1f,1f) * Time.deltaTime;
        if (wormwholeai.Alive == false)
        {
            tf.Rotate(new Vector3(0f, 0f, RotSpeed * Time.deltaTime));
        }

        if (wormwholeai.Health <= 0 && ShotOff == false)
        {
            AudioManager.instance.ChangePitch("WormDie", Random.Range(1f, 0.5f));
            AudioManager.instance.Play("WormDie");
            objectPooler.SpawnFromPool("Blood", tf.position, Quaternion.identity);
            RotSpeed = Random.Range(-3600f, 3600f);
            XVel = Random.Range(-25f, 25f);
            ShotOff = true;
            boxCollider.size = new Vector2(0f, 0f);
            wormwholeai.Alive = false;
        }

        tf.position += new Vector3(XVel,0f,0f) * Time.deltaTime;
        XVel *= 0.98f;
    }

}
