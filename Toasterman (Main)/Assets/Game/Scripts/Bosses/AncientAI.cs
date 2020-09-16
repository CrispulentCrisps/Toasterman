using UnityEngine;

public class AncientAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform[] BodyParts;

    private int State;
    public int SineOffset;
    public float Health;
    private float SineTime;
    private float SineFreq;
    private float SineAmp;
    private float angle;

    public bool IntroDone = false;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        SineFreq = 3f;
        SineAmp = 1f;

    }

    public void OnObjectSpawn()
    {

        SineFreq = 3f;
        SineAmp = 1f;

    }
    // Update is called once per frame
    void Update()
    {
        if (IntroDone == true)
        {
            SineTime += Time.deltaTime;

            if (SineAmp < 7.5f)
            {

                SineAmp += 2.5f * Time.deltaTime;

            }

            for (int i = 0; i < 2; i++)//Hands
            {
                BodyParts[i].position = new Vector3(SineAmp * Mathf.Sin(SineTime * SineFreq - SineOffset)* 1.5f, SineAmp * Mathf.Cos(SineTime * SineFreq - SineOffset) * 0.75f, 0f);
                BodyParts[i].Rotate(0, 0, BodyParts[i].position.y);
                if (i == 1)
                {
                    BodyParts[i].position = new Vector3(SineAmp * Mathf.Sin(SineTime * SineFreq + SineOffset) * -1.5f, SineAmp * Mathf.Cos(SineTime * SineFreq + SineOffset) * -.75f, 0f);
                    BodyParts[i].Rotate(0, 0, BodyParts[i].position.y * -1f);
                }
            }
        }


    }

    void FixedUpdate()
    {



    }


    public void IntroComplete()
    {

        IntroDone = true;

    }

    public void ShootCircle(int BulletAmount, string BulletName)
    {
        for (int i = 0; i < BulletAmount; i++)
        {
            angle = i * Mathf.PI * 2f / BulletAmount;//Converts angle to radians

        }
        
    }

}
