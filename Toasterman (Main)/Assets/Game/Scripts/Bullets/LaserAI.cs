using UnityEngine;

public class LaserAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Animator anim;

    public Transform tf;
    public float Lifetime;
    private float Lifeline;

    private float CurrentSpeed;
    private float RotVel;
    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }
  
    public void OnObjectSpawn()
    {
        RotVel = 0;
        CurrentSpeed = 0;
        anim.SetTrigger("Enter");
        Lifeline = 0;
        FindObjectOfType<AudioManager>().Play("Laser fire");
    }
    // Update is called once per frame
    void Update()
    {
        RotVel += RotateSpeed * Time.deltaTime;
        CurrentSpeed += RotVel * Time.deltaTime;
        Lifeline += Time.deltaTime;
        if (Lifeline >= Lifetime)
        {
            anim.SetTrigger("Exit");
        }
    }

    void FixedUpdate()
    {
        tf.RotateAround(new Vector3(0,0,0),Vector3.forward, CurrentSpeed * Time.deltaTime);
    }

    public void SetDead()
    {

        gameObject.SetActive(false);

    }
}
