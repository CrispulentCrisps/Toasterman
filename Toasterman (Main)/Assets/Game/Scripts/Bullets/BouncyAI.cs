using UnityEngine;

public class BouncyAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    private GameObject Target;

    private Shooting shooting;

    private Vector2 Movement;

    private float speed = 0;
    
    public int BounceAmount;

    public float RotMinMax;

    public void OnObjectSpawn()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        shooting = Target.GetComponent<Shooting>();
        speed = 17.5f;

    }

    private void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        BounceAmount = shooting.BulletLevel + 2;

        Movement = new Vector2(speed, 0);

        if (BounceAmount <= 0 || tf.position.x >= 20 || tf.position.x <= -20 || tf.position.y >= 20 || tf.position.y <= -20)
        {

            gameObject.SetActive(false);

        }
    }

    void FixedUpdate()
    {

        tf.Translate(Movement * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("NotSoGoodThing"))
        {

            tf.Rotate(0, 0, Random.Range(-RotMinMax, RotMinMax) - 180);
            BounceAmount--;

        }

    }

}
