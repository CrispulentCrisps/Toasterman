using UnityEngine;

public class RotateY : MonoBehaviour
{
    public Transform tf;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {

        tf.Rotate(new Vector3(2 * Mathf.Sin(speed), Mathf.Tan(speed), speed * 0.5f));

    }
}
