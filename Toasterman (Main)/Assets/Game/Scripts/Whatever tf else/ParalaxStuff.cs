using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParalaxStuff : MonoBehaviour
{
    public GameObject[] Layer;

    public Vector3[] FrontPos;

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
            switch (i)
            {
                default: FrontPos[i] -= new Vector3(paraspeed * Time.deltaTime, 0, 0);
                    break;
                case 0: FrontPos[i] -= new Vector3(paraspeed * Time.deltaTime, 0, 0);
                    break;
                case 1:
                    FrontPos[i] -= new Vector3(paraspeed * Time.deltaTime, 0, 0);
                    break;
                case 2:
                    FrontPos[i] -= new Vector3(paraspeed / ParaDampen * Time.deltaTime, 0, 0);
                    break;
                case 3:
                    FrontPos[i] -= new Vector3(paraspeed / ParaDampen * Time.deltaTime, 0, 0);
                    break;
                case 4:
                    FrontPos[i] -= new Vector3(paraspeed / (ParaDampen * 2) * Time.deltaTime, 0, 0);
                    break;
                case 5:
                    FrontPos[i] -= new Vector3(paraspeed / (ParaDampen * 2) * Time.deltaTime, 0, 0);
                    break;
                case 6:
                    FrontPos[i] -= new Vector3(paraspeed / (ParaDampen * 4) * Time.deltaTime, 0, 0);
                    break;
                case 7:
                    FrontPos[i] -= new Vector3(paraspeed / (ParaDampen * 4) * Time.deltaTime, 0, 0);
                    break;
            }
            Layer[i].transform.position = FrontPos[i];
        }
    }
}
