using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform tf;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

    }
}
