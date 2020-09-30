using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParalaxMovement : MonoBehaviour
{

    public GameObject[] Layer;

    public Vector3[] FrontPos;

    public int BackThresh;

    public float paraspeed;
    public float paraspeedGoal;
    public float paraspeedIncrement;
    public float ParaDampen;


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

        }

        if (paraspeed > paraspeedGoal)
        {

            paraspeed -= paraspeedIncrement;

        }



        for (int i = 0; i < Layer.Length; i++)
        {

            if (Layer[i].transform.position.x <= -BackThresh)
            {
                FrontPos[i] += new Vector3(BackThresh * 2, 0, 0);
                Layer[i].transform.position = FrontPos[0];

            }

            else if (Layer[i].transform.position.x >= BackThresh)
            {
                FrontPos[i] -= new Vector3(BackThresh * 2, 0, 0);
                Layer[i].transform.position = FrontPos[0];

            }

        }

    }

    // Update is called once per few frames
    void FixedUpdate()
    {

        for (int i = 0; i < Layer.Length; i++)
        {

            if (i == 0 || i == 1)
            {

                FrontPos[i] -= new Vector3(paraspeed * Time.deltaTime, 0, 0);

            }
            else if (i == 2 || i == 3)
            {

                FrontPos[i] -= new Vector3(paraspeed / ParaDampen * Time.deltaTime, 0, 0);

            }
            else if (i == 4 || i == 5)
            {

                FrontPos[i] -= new Vector3(paraspeed / (ParaDampen * 2) * Time.deltaTime, 0, 0);

            }
            else if (i == 6 || i == 7)
            {

                FrontPos[i] += new Vector3(paraspeed / (ParaDampen * 2) * Time.deltaTime, 0, 0);

            }

            Layer[i].transform.position = FrontPos[i];

        }

    }
}
