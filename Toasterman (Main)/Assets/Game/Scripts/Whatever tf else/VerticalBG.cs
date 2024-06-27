using UnityEngine;

public class VerticalBG : ParalaxStuff
{
    void Start()
    {
        Sr = new SpriteRenderer[Layer.Length];
        LayerTf = new Transform[Layer.Length];
        Length = new float[Layer.Length];
        for (int i = 0; i < Layer.Length; i++)
        {
            Sr[i] = Layer[i].GetComponent<SpriteRenderer>();
            Length[i] = Sr[i].bounds.size.y;
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

        }
        else if (paraspeed > paraspeedGoal)
        {
            paraspeed -= paraspeedIncrement;
        }
    }
    public virtual void VerticalMoveBG()
    {
        for (int i = 0; i < Layer.Length; i++)
        {
            LayerTf[i].position -= new Vector3(0f, paraspeed / ParaDampen[i] * Time.deltaTime, 0);
        }
        for (int i = 0; i < Layer.Length; i++)
        {
            if (LayerTf[i].position.y < -Length[i] || LayerTf[i].position.y > Length[i])
            {
                LayerTf[i].position -= new Vector3(0f, Length[i] * 2);
            }
        }
    }
    private void FixedUpdate()
    {
        VerticalMoveBG();
    }
}
