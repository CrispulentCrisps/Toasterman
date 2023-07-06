using UnityEngine;

public class HelperHatAI : MonoBehaviour, IPooledObject
{
    public Transform tf;

    private Animator anim;

    private Vector2 Movement;

    private float Yvel;
    private float Xvel;
    private float t;

    private bool Thrown;
    public string[] HelpNames;

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
        EnemyScript.EnemyAmount++;
        Yvel = 15.5f;
        Xvel = 9f;
        tf.position = new Vector3(-13f, -10f, 0f);
        Thrown = false;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t >= 0.1f)
        {
            objectPooler.SpawnFromPool("HelperHatTrail", tf.position, Quaternion.identity);
        }

        Movement = new Vector2(Xvel, Yvel);
        Yvel -= 9.81f * Time.deltaTime;
        if (Thrown == false && tf.position.y > 0f)
        {
            anim.SetTrigger("PowerShoot");
            Thrown = true;
        }
        if (tf.position.y > -20f)
        {
            gameObject.active = false;
            EnemyScript.EnemyAmount--;
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

    public void SpawnPower()
    {
        objectPooler.SpawnFromPool(HelpNames[EnemyScript.PowerNum], tf.position, Quaternion.identity);
    }

}