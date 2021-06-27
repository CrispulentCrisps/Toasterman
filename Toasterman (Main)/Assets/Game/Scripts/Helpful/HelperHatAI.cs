using UnityEngine;

public class HelperHatAI : MonoBehaviour, IPooledObject
{
    public Transform tf;

    private Animator anim;

    private Vector2 Movement;

    private float Yvel;
    private float Xvel;

    private bool Thrown;

    ObjectPools objectPooler;

    void Start()
    {

        objectPooler = ObjectPools.Instance;
        Yvel = 15.5f;
        Xvel = 9f;
        tf.position = new Vector3(-13f, -10f, 0f);
        Thrown = false;
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnObjectSpawn()
    {
        Yvel = 15.5f;
        Xvel = 9f;
        tf.position = new Vector3(-13f, -10f, 0f);
        Thrown = false;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Movement = new Vector2(Xvel, Yvel);
        Yvel -= 9.81f * Time.deltaTime;
        if (Thrown == false && tf.position.y > 0f)
        {
            anim.SetTrigger("PowerShoot");
            Thrown = true;
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

    public void SpawnPower()
    {
        objectPooler.SpawnFromPool("BulletAdd", tf.position, Quaternion.identity);
    }

}