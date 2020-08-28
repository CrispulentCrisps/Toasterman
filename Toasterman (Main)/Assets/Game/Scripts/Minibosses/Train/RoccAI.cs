using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoccAI : MonoBehaviour
{

    public Transform tf;

    public bool Ending;

    public TrainAI trainAI;

    public float MovementSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Ending == true)
        {

            tf.position += new Vector3(MovementSpeed, 0, 0) * Time.deltaTime;

            if (tf.position.x <= -50f)
            {

                trainAI.Ended = true;

            }

        }

    }
}
