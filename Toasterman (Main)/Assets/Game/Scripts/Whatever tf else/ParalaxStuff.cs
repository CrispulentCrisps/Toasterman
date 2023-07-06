using System.Collections;
using UnityEngine;

public class ParalaxStuff : MonoBehaviour
{
    public GameObject[] Layer;
    Transform[] LayerTf;

    SpriteRenderer[] Sr;

    public float[] ParaDampen;
    public float[] YPos;
    public float paraspeed;
    public float paraspeedGoal;
    public float paraspeedIncrement;
    public static float BGSpeed;
    float[] Length;

    // Start is called before the first frame update
    void Start()
    {
        Sr = new SpriteRenderer[Layer.Length];
        LayerTf = new Transform[Layer.Length];
        Length = new float[Layer.Length];
        for (int i = 0; i < Layer.Length; i++)
        {
            Sr[i] = Layer[i].GetComponent<SpriteRenderer>();
            Length[i] = Sr[i].bounds.size.x;
            LayerTf[i] = Layer[i].transform;
        }
    }

    // Updates every frame
    void Update()
    {
        BGSpeed = paraspeed;
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
            LayerTf[i].position -= new Vector3(paraspeed / ParaDampen[i] * Time.deltaTime, 0f, 0);
        }
        for (int i = 0; i < Layer.Length; i++)
        {
            if (LayerTf[i].position.x < -Length[i] || LayerTf[i].position.x > Length[i])
            {
                LayerTf[i].position += new Vector3(Length[i]*2, 0f);
            }
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
                float YPosA = LayerTf[i].position.y;
                //FrontPos[i] = new Vector3(FrontPos[i].x, Mathf.Lerp(YPosA, YPosB[i], ACurve.Evaluate(Time.deltaTime / seconds)), FrontPos[i].z);
                LayerTf[i].position = new Vector3(LayerTf[i].position.x, Mathf.Lerp(YPosA, YPosB[i], ACurve.Evaluate(Time.deltaTime / seconds)), LayerTf[i].position.z);
                //LayerTf[i].position = FrontPos[i];
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
