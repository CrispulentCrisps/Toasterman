using UnityEngine;

public class BoomerangScript : MonoBehaviour, IPooledObject
{

    public Rigidbody2D rb;

    public Transform tf;

    public GameObject Ship;
    public GameObject WaveMaker;

    public EnemyScript enemyscript;

    public Animator Anim;

    public Vector2 speed;

    private bool Happy = true;

    private float YDistance;
    public float Health;

    ObjectPools objectPooler;

    private int I; // Wave number
    public float RotSpeed;

    public void OnObjectSpawn()
    {
        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        objectPooler = ObjectPools.Instance;
        Ship = GameObject.Find("Ship");
        speed = new Vector2(enemyscript.Waves[I].EnemySpeed * enemyscript.Waves[I].Inverse, 0);
        RotSpeed = 45f;
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position - speed * Time.deltaTime); //DO NOT CHANGE !!!!!!!
        tf.Rotate(0,0,RotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Health = 0f;
        }
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            if (Health <= 0f)
            {
                Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    public void Turn()
    {
        Anim.SetTrigger("Behind");
        YDistance = (tf.position.y - Ship.transform.position.y + Random.Range(0.5f, -0.5f));
        speed = new Vector2(speed.x, speed.y + YDistance);
        Happy = false;
    }

    void Update()
    {
        //Turn around and hit player
        if (tf.position.x <= Ship.transform.position.x - 2 && Happy == true)
        {
            Turn();
        }
        if (tf.position.x <= -15 && Happy == false)
        {
            gameObject.SetActive(false);
        }
        if (Happy == false)
        {
            RotSpeed -= 360f * Time.deltaTime;
            speed.x -= 0.25f;
        }
    }

}
