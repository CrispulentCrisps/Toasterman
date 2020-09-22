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
    private float TSpace;

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

        TSpace += Time.deltaTime;

        if (TSpace >= 3f)
        {

            State = Random.Range(0, 1);

        }

        if (State == 1)
        {



        }

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

    public void IntroComplete()
    {

        IntroDone = true;

    }

    public void ShootCircle(int BulletAmount, string BulletName, Transform tf)
    {
        for (int i = 0; i < BulletAmount; i++)
        {
            angle = i * Mathf.PI * 2f / BulletAmount;//Converts angle to radians
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0,0, angle));
        }
        
    }

}
