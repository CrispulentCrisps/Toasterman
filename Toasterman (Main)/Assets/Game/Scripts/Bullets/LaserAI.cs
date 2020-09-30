using UnityEngine;

public class LaserAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform tf;

    public AnimationCurve RotateSpeed;
    private float FinalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }
  
    public void OnObjectSpawn()
    { 
    
    }
    // Update is called once per frame
    void Update()
    {
        FinalSpeed = RotateSpeed.Evaluate(Time.time);
    }

    void FixedUpdate()
    {
        tf.RotateAroundLocal(Vector3.forward, FinalSpeed);
    }
}
