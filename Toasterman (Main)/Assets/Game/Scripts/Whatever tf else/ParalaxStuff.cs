using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParalaxStuff : MonoBehaviour
{
    public GameObject[] Layer;

    public Vector3[] FrontPos;

    public float[] ParaDampen;

    public float paraspeed;
    public float paraspeedGoal;
    public float paraspeedIncrement;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FrontPos.Length; i++)
        {
            FrontPos[i] = Layer[i].transform.position; // set positions
        }

    }

    // Updates every frame
    void Update()
    {
        if (paraspeed < paraspeedGoal)
        {
            paraspeed += paraspeedIncrement;

        }else if (paraspeed > paraspeedGoal)
        {
            paraspeed -= paraspeedIncrement;
        }
    }

    // Update is called once per few frames
    void FixedUpdate()
    {
        for (int i = 0; i < Layer.Length; i++)
        {
            if (Layer[i].transform.position.x <= -30)
            {
                FrontPos[i] += new Vector3(60, 0, 0);
                Layer[i].transform.position = FrontPos[0];
            }
            else if (Layer[i].transform.position.x >= 30)
            {
                FrontPos[i] -= new Vector3(60, 0, 0);
                Layer[i].transform.position = FrontPos[0];
            }
        }
        for (int i = 0; i < Layer.Length; i++)
        {
            FrontPos[i] -= new Vector3(paraspeed / ParaDampen[i] * Time.deltaTime, 0, 0);
            Layer[i].transform.position = FrontPos[i];
        }
    }
}
