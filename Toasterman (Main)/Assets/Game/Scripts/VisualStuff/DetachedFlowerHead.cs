using UnityEngine;

public class DetachedFlowerHead : MonoBehaviour, IPooledObject
{
    protected ObjectPools objectPooler;
    private Vector2 Movement;
    [SerializeField] private Transform FlowerHead;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        Movement = new Vector2(Random.Range(-3f, 3f), ParalaxStuff.BGSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement -= new Vector2(0f, ParalaxStuff.BGSpeed*4) * Time.deltaTime;
        transform.Translate(Movement * Time.deltaTime);
        FlowerHead.Rotate(new Vector3(0f, 0f, (Movement.x*45f) * Time.deltaTime));
        if (transform.position.y < -30f) gameObject.active = false;

    }
}
