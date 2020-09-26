using UnityEngine;

public class AncientAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Animator Anim;

    public Transform[] BodyParts;

    public int State;
    public int SineOffset;
    public int BulletAmount;
    private int j;//For recursion in shooting rocks

    public float Health;
    private float SineTime;
    private float SineFreq;
    private float SineAmp;
    private float angle;
    private float TSpace;
    private float TimingSpaceRock;
    
    private bool Shooting;

    public bool IntroDone = false;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        SineFreq = 2f;
        SineAmp = 1f;
        j = 0;
    }

    public void OnObjectSpawn()
    {
        SineFreq = 2f;
        SineAmp = 1f;
        j = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (IntroDone == true)
        {

            TSpace += Time.deltaTime;

            if (TSpace >= 1f)
            {

                State = Random.Range(0, 2);
                TSpace = 0;
            }

            switch (State)
            {
                case 1:
                    Anim.SetTrigger("Rock");
                    State = 0;
                    break;
            }

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

        if (Shooting == true)
        {

            if (j >= 4 )
            {

                Shooting = false;
                j = 0;
            }

            TimingSpaceRock += Time.deltaTime;
            
            if (TimingSpaceRock > 0.5f)
            {

                for (int i = 0; i < BulletAmount; i++)
                {

                    ShootCircle(BulletAmount, "Rock", BodyParts[i % 2]);

                }

                TimingSpaceRock = 0;
                j++;

            }
        }

    }

    public void ShootCircle(int BulletAmount, string BulletName, Transform tf)
    {
        angle = 0f;
        for (int i = 0; i < BulletAmount; i++)
        {

            float AngleStep = 360f / BulletAmount;
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
        }
        
    }

    public void Shoot()
    {

        Shooting = true;

    }

    public void IntroComplete()
    {

        IntroDone = true;

    }

}
