using UnityEngine;

public class AcidAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    private GameObject Target;

    private Vector2 Movement;

    private float speedx = 0;
    private float speedy = 0;

    public void OnObjectSpawn()
    {

        Target = GameObject.FindGameObjectWithTag("Player");
        Shooting shooting = Target.GetComponent<Shooting>();


        tf.position = Target.transform.position;

        speedx = Random.Range(11f, 14f);
        speedy = Random.Range(shooting.BulletLevel * -.9f, shooting.BulletLevel * .9f);

    }

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void Update()
    {

        speedx *= 0.925f;

        Movement = new Vector2(speedx, speedy);

        if (speedx <= 0.9f)
        {

            speedx = 0f;
            speedy = 0f;
            tf.position = Target.transform.position;
            gameObject.SetActive(false);

        }

    }

    void FixedUpdate()
    {

        tf.Translate(Movement * Time.deltaTime);

    }

}
