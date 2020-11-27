using UnityEngine;

public class BulletAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    
    private Vector2 Movement;

    public float speedx = 11;
    public float speedy = 0;
    public float RotSPeed;
    private float BulletRot;

    private int Length;

    public bool Specifics;
    public bool Killable;
    public bool Turn;

    public string[] CollisionNames;

    ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Specifics == true)
           {

            Length = CollisionNames.Length;

            for (int i = 0; i < Length; i++)
            {
                if (coll.gameObject.CompareTag(CollisionNames[i]))
                {

                    if (Killable == true)
                    {

                        objectPooler.SpawnFromPool("BulletHit", tf.position, Quaternion.identity);

                        gameObject.SetActive(false);

                    }

                }

            }
        }

    }

    void Update()
    {
        Movement = new Vector2(speedx, speedy);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
        
        if (Turn)
        {
            tf.Rotate(0f, 0f, RotSPeed);
        }

        if (tf.position.x >= 18f || tf.position.x <= -18f || tf.position.y >= 10f || tf.position.y <= -10f)
        {
            gameObject.SetActive(false);
        }
    }

}
