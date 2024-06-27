using UnityEngine;

public class SporeBomb : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    public string BulletName;

    private float YVel;
    private float XVel;
    private float XAmp;
    private float YAmp;
    private float SpeedMult;

    private Vector2 SineMove;
    private Vector2 Movement;

    private GameObject Ship;

    private Animator Anim;

    private bool Alive = true;

    private Quaternion BulletRot;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        tf.position = new Vector3(Random.Range(-12f,15f),12f,0f);
    }

    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        Ship = GameObject.Find("Ship");//gets the game object
        Anim = gameObject.GetComponent<Animator>();
        Alive = true;
        XAmp = Random.Range(10f, 1f);
        YAmp = Random.Range(5f, 1f);
        SpeedMult = Random.Range(1f, 4f);
    }

    void Update()
    {
        if (Alive == true)
        {
            YVel = YAmp * Mathf.Sin(SineMove.y) - YAmp * 0.5f;
            XVel = XAmp * Mathf.Cos(SineMove.x);

            SineMove += new Vector2(Time.deltaTime, Time.deltaTime) * SpeedMult;

            Movement = new Vector2(XVel, YVel);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && Alive == true)
        {
            Anim.SetTrigger("Blow");
            Alive = false;
        }

    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

    public void ShootSpore(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {

            objectPooler.SpawnFromPool(BulletName,tf.position,Quaternion.identity);
        }
    }

    public void Alivent()
    {
        EnemyScript.EnemyAmount--;
        gameObject.SetActive(false);
    }
}
