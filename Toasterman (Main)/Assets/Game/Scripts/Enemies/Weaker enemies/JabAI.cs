using UnityEngine;

public class JabAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public EnemyScript enemyscript;

    public Transform tf;
    private Transform Target;

    public GameObject Ship;

    public Animator Anim;

    private Vector2 Movement;

    public float JabSpeed;
    public float Health;

    private bool TargetLocked;

    private int I;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    #region SpawningCode
    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        tf = transform;
        GameObject WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object
        enemyscript = WaveMaker.GetComponent<EnemyScript>();// gets the scripts for the wave makers
        I = enemyscript.i;
        Ship = GameObject.Find("Ship");
        Target = Ship.GetComponent<Transform>();
        Movement = new Vector2(enemyscript.Waves[I].EnemySpeed, 0);
    }
    #endregion

    #region CollisionCode
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Health = 0f;
        }
        else if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
        }

        if (Health <= 0)
        {
            EnemyScript.EnemyAmount--;
            Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
            objectPooler.SpawnFromPool("Boom", tf.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
    #endregion

    void Update()
    {
        //Debug.Assert(gameObject.active);

        TargetLocked = true;
        
        if (TargetLocked == true)
        {
            Movement *= new Vector2(0.9f, 0.75f);
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }

    public void StopAndLook()
    {
        Movement = new Vector2(0f, 0f);
        //look at by https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        Vector3 difference = Target.position - tf.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 180);
    }

    public void Jab()
    {
        Movement -= new Vector2(JabSpeed, 0f);
    }
}
