using UnityEngine;

public class LizarmosV2AI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;
    public Transform[] ShootPoints;
    public Transform Target;
    public float MoveSpeed;
    private float P;
    public void OnObjectSpawn()
    {
        Target = FindObjectOfType<PlayerMovement>().transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        Target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        P += Time.deltaTime;
        transform.position = new Vector3(12f + (2 * Mathf.Sin(P * MoveSpeed)), transform.position.y, transform.position.z);
        if (Target.position.y < transform.position.y)
        {
            transform.position -= new Vector3(0, MoveSpeed * Time.deltaTime, 0);
        }
        else if (Target.position.y > transform.position.y)
        {
            transform.position += new Vector3(0, MoveSpeed * Time.deltaTime, 0);
        }
    }

    public void Attack1(int count)
    {
        BulletPatternsModule.ShootArc(135, count, "Missle", ShootPoints[5], 0);
    }
}
