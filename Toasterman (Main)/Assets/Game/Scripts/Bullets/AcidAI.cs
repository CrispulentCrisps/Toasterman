using UnityEngine;

public class AcidAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    private Vector2 Movement;

    private float speedx = 0;
    private float speedy = 0;

    private int LifeTimeHits = 3;

    public void OnObjectSpawn()
    {
        speedx = Random.Range(11f, 14f);
        speedy = Random.Range(-5f,5f);
        LifeTimeHits = 3;
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        speedx *= 0.95f;
        speedy *= 0.95f;
        Movement = new Vector2(speedx, speedy);
        if (speedx <= 0.9f)
        {
            speedx = 0f;
            speedy = 0f;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("NotSoGoodThing"))
        {
            LifeTimeHits--;
            if (LifeTimeHits <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }
}
