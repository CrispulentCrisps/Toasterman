using UnityEngine;

public class BouncyAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    private GameObject Target;

    private Vector2 Movement;

    private float speed = 0;
    
    public int BounceAmount;

    public float RotMinMax;

    public void OnObjectSpawn()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        speed = 22.5f;
    }

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        BounceAmount = (Shooting.BulletLevel[2]) + 2;

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
