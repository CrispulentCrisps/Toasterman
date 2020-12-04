using UnityEngine;

public class BulletAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    
    private Vector2 Movement;

    private float speedx;
    private float speedy;
    public float AccX;
    public float AccY;
    public float AccMinX;
    public float AccMinY;
    public float speedxMem;
    public float speedyMem;
    private float BulletRot;
    private float ST;

    private int Length;

    public bool Specifics;
    public bool Killable;
    public bool ChangeAcc;

    public string[] CollisionNames;

    ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        speedxMem = speedx;
        speedyMem = speedy;
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        Debug.Log("speedxMem:" + speedxMem + " speedyMem:" + speedyMem);
        Movement = new Vector2(speedx, speedy);
        if (DEBUG.ChangeGraphics == true)
        {
            SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
            rend.sprite = Resources.Load<Sprite>("Toast");
        }
        speedx = speedxMem;
        speedy = speedyMem;
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
        if (ChangeAcc)
        {
            speedx -= AccX * Time.deltaTime;
            speedy -= AccY * Time.deltaTime;
            if (speedx <= -AccMinX || speedx >= AccMinX || speedy <= -AccMinY || speedy >= AccMinY)
            {
                objectPooler.SpawnFromPool("BulletHit", tf.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

}
