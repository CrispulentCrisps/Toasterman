using UnityEngine;

public class SegmentAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    public BoxCollider2D boxcol;

    public Animator anim;

    private bool StartedSegment = false;

    public float MovementSpeed;

    public float YSpeed;

    public bool Alive = true;
    private bool Gravity;

    private float RotationAmount;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    public void OnObjectSpawn()
    {

        tf.position -= new Vector3(-16, -3.5f, 0);

        StartedSegment = false;

        boxcol.enabled = true;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("BigStone"))
        {

            Alive = false;

            YSpeed = Random.Range(10f, 20f);
            MovementSpeed = Random.Range(-1f, -10f);
            RotationAmount = Random.Range(-135f,135f);
            boxcol.enabled = false;
            Gravity = true;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Alive == true)
        {

            if (tf.position.x >= -15f && StartedSegment == false)
            {

                anim.Play("SegmentMoving");

                StartedSegment = true;

            }
            else if (tf.position.x >= 15f)
            {

                tf.position -= new Vector3(30f, 0, 0);

            }

        }
        else if (Gravity == true)
        {

            YSpeed -= 9.81f * Time.deltaTime;

            tf.Rotate(0, 0, RotationAmount);

            if (tf.position.y <= -10f + tf.localScale.x)
            {

                gameObject.SetActive(false);

            }

        }

        tf.position += new Vector3(MovementSpeed, YSpeed, 0) * Time.deltaTime;
    }

}
