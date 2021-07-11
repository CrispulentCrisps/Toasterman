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
    private float SizeDiv;

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
        LifeTime = Random.Range(3f, 5f);

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && playermovement.Dashin == false)
        {
            AudioManager.instance.Play("Shroomed");
            Attached = true;
            LifeTime = Random.Range(3f, 5f);
            SizeDiv = LifeTime;
        }
    }

    void Update()
    {
        LifeTime -= Time.deltaTime;

        if (LifeTime <= 0f)
        {
            tf.localScale = new Vector3(1 - (1/SizeDiv),1 - (1 / SizeDiv), 1f);
            playermovement.Inverse = false;
            Attached = false;
            gameObject.SetActive(false);
        }
        else if (tf.position.x >= 18f || tf.position.x <= -18f || tf.position.y >= 10f || tf.position.y <= -10f)
        {
            gameObject.SetActive(false);
        }

        if (playermovement.Dashin == true && Attached == true)
        {
            Attached = false;
            playermovement.Inverse = false;
            camerashake.SetAbberation(0f);
            ShotOff = true;
        }
        else if (Attached == true)
        {
            ShotOff = false;
            LifeTime = Random.Range(2.5f, 5f);
            playermovement.Inverse = true;
            camerashake.SetAbberation(1f);
        }

        if (ShotOff == true)
        {
            speedx = playermovement.Movement.x * -1f + Random.Range(-10f, 10f);
            speedy = playermovement.Movement.y * -1f + Random.Range(-10f, 10f);
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
