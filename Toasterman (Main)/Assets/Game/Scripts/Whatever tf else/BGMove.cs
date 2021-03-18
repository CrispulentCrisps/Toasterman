using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public Transform tf;

    public float paraspeed;
    public float paraspeedGoal;
    public float paraspeedIncrement;
    public float ParaDampen;
    public float Bounds;
    [Header("True for X, False for Y")]
    public bool XorY;

    // Update is called once per frame
    void Update()
    {
        if (paraspeed < paraspeedGoal)
        {
            paraspeed += paraspeedIncrement * Time.deltaTime;

        }
        else if (paraspeed > paraspeedGoal)
        {
            paraspeed -= paraspeedIncrement * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (XorY)
        {
            tf.position -= new Vector3(paraspeed / ParaDampen, 0f, 0f) * Time.deltaTime;
        }
        else if (!XorY)
        {
            tf.position -= new Vector3(0f, paraspeed / ParaDampen, 0f) * Time.deltaTime;
        }

        if (tf.position.x < -Bounds)
        {
            tf.position += new Vector3(Bounds * 2f, 0f, 0f);
        }
        else if (tf.position.x > Bounds)
        {
            tf.position -= new Vector3(Bounds * 2f, 0f, 0f);
        }
        else if (tf.position.y < -Bounds)
        {
            tf.position += new Vector3(0f, Bounds * 2f, 0f);
        }
        else if (tf.position.x > Bounds)
        {
            tf.position -= new Vector3(0f, Bounds * 2f, 0f);
        }
    }
}
