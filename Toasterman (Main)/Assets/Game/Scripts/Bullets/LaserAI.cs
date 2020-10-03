using UnityEngine;

public class LaserAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Animator anim;

    public Transform tf;
    public float Lifetime;
    private float Lifeline;

    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }
  
    public void OnObjectSpawn()
    {
        RotateSpeed = 0;
        anim.SetTrigger("Enter");
        Lifeline = 0;
    }
    // Update is called once per frame
    void Update()
    {
        RotateSpeed += 50 * Time.deltaTime;
        Lifeline += Time.deltaTime;
        if (Lifeline >= Lifetime)
        {
            anim.SetTrigger("Exit");
        }
    }

    void FixedUpdate()
    {
        tf.RotateAround(new Vector3(0,0,0),Vector3.forward, RotateSpeed * Time.deltaTime);
    }

    public void SetDead()
    {

        gameObject.SetActive(false);

    }
}
