using UnityEngine;

public class PowerUp : MonoBehaviour, IPooledObject
{
    public Transform tf;

    private Vector2 Movement;

    public Shooting shooting;

    private GameObject Target;

    public bool StaticPower;

    private float YVel;
    private float XVel;

    public float Speed;
    public float FallSpeedMult;

    public int PowerData; 
    // check if the power type is the same
    // if the powers are different then you should set the bullet type to that power at level 1 of 4

    ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        Target = GameObject.Find("Ship");//gets the game object
        YVel = Random.Range(Speed, Speed * 2f);
        XVel = Random.Range(Speed, -Speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play("Get power");
            Shooting.BulletLevel[PowerData]++;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement = new Vector2(XVel, YVel);
        YVel -= FallSpeedMult * 9.81f * Time.deltaTime;
        if (tf.position.y <= -10f && !StaticPower)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

}
