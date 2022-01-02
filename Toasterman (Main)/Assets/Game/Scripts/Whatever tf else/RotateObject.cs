using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform tf;
    public float Speed;
    public int[] AxisMult;

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(Speed * AxisMult[0] * Time.deltaTime, Speed * AxisMult[1] * Time.deltaTime, Speed * AxisMult[2] * Time.deltaTime);
    }
}
