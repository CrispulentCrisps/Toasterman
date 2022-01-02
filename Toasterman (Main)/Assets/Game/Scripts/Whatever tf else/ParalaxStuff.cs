using System.Collections;
using UnityEngine;

public class ParalaxStuff : MonoBehaviour
{
    public GameObject[] Layer;

    public Vector3[] FrontPos;

    public float[] ParaDampen;

    public float[] YPos;

    public float paraspeed;
    public float paraspeedGoal;
    public float paraspeedIncrement;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FrontPos.Length; i++)
        {
            FrontPos[i] = Layer[i].transform.position; // set positions
            YPos[i] = FrontPos[i].y;
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
            FrontPos[i] -= new Vector3(paraspeed / ParaDampen[i] * Time.deltaTime, 0f, 0);
            Layer[i].transform.position = FrontPos[i];
        }
    }

    public IEnumerator MoveYAToB(float seconds, float[] YPosB, AnimationCurve ACurve)
    {
        float t = 0f;  //time value for the ACurve.Evaluate
        while (ACurve.Evaluate(Time.deltaTime / seconds) < 1)
        {
            t += Time.deltaTime;
            for (int i = 0; i < Layer.Length; i++)
            {
                float YPosA = FrontPos[i].y;
                FrontPos[i] = new Vector3(FrontPos[i].x, Mathf.Lerp(YPosA, YPosB[i], ACurve.Evaluate(Time.deltaTime / seconds)), FrontPos[i].z);
                Layer[i].transform.position = FrontPos[i];
            }
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("DONE");
    }
}
