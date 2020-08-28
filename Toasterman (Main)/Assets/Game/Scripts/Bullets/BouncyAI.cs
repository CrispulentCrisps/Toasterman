using UnityEngine;

public class BouncyAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public Transform tf;

    private GameObject Target;

    private Vector2 Movement;

    private float speedx = 0;
    private float speedy = 0;
    
    public int BounceAmount;

    public void OnObjectSpawn()
    {

        speedx = 5;
        speedy = 0;

    }

    private void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    void FixedUpdate()
    {

        tf.Translate(Movement * Time.deltaTime);

    }


}
