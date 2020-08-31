using UnityEngine;

public class SporeAI : MonoBehaviour, IPooledObject
{
    public Transform tf;

    private Vector2 Movement;

    public float speedx;
    public float speedy;
    private float BulletRot;

    public PlayerMovement playermovement;

    public CameraShake camerashake;

    private GameObject Target;

    public bool Attached;
    private bool ShotOff;

    private float LifeTime;

    ObjectPools objectPooler;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {

        Movement = new Vector2(speedx, speedy);

        Target = GameObject.FindGameObjectWithTag("Player");

        playermovement = Target.GetComponent<PlayerMovement>();

        camerashake = Camera.main.GetComponent<CameraShake>();

        speedy = Random.Range(3f,10f);
        LifeTime = Random.Range(20f, 40f);

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && playermovement.Dashin == false)
        {

            Attached = true;
            LifeTime = Random.Range(20f, 40f);

        }

    }

    void Update()
    {

        LifeTime -= Time.deltaTime;

        if (LifeTime <= 0f)
        {

            playermovement.Inverse = 0;
            gameObject.SetActive(false);


        }
        else if (tf.position.x >= 18f || tf.position.x <= -18f || tf.position.y >= 10f || tf.position.y <= -10f)
        {

            gameObject.SetActive(false);

        }

        if (playermovement.Dashin == true)
        {

            Attached = false;
            
            playermovement.Inverse = 0;
            camerashake.SetAbberation(0f);
        }
        else if (Attached == true)
        {

            playermovement.Inverse = 1;
            ShotOff = true;
            camerashake.SetAbberation(1f);
        }

        if (ShotOff == true)
        {

            Movement = playermovement.Movement * -1f;

            ShotOff = false;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);

        if (Attached == false)
        {

            Movement = new Vector2(speedx + Random.Range(-5f, 5f), speedy + Random.Range(-7.5f, -2.5f));

        }
        else
        {
            tf.position = Target.transform.position + new Vector3(Random.Range(0.25f,-0.25f),Random.Range(0.25f, -0.25f),0f);
        }

    }

}
