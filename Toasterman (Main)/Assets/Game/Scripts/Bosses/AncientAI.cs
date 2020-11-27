using UnityEngine;

public class AncientAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public GameObject BG;

    public Animator Anim;
    public Animator EyeAnim;
    public Animator ShroomAnim;

    public CameraShake camerashake;
    public ParalaxStuff paralaxstuff;
    public HandScript[] handscripts;

    public GameObject[] Hands;

    public Transform[] BodyParts;

    private string BulletName;

    public int State;
    public int SineOffset;
    public int BulletAmount;
    public int ShootingType;
    private int MaxState;
    private int MinState;
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
        handscripts[0] = Hands[0].GetComponent<HandScript>();
        handscripts[1] = Hands[1].GetComponent<HandScript>();
    }

    public void OnObjectSpawn()
    {
        gameObject.transform.Rotate(0f, -90f, 0f);
        camerashake = Camera.main.GetComponent<CameraShake>();
        BG = GameObject.Find("BG stuff");
        paralaxstuff = BG.GetComponent<ParalaxStuff>();
        SineFreq = 2f;
        SineAmp = 1f;
        j = 0;
        ShroomAnim = GameObject.Find("BigMushroom").GetComponent<Animator>();
        StartCoroutine(FindObjectOfType<AudioManager>().FadeAudio("How shroomy are you?", 1f));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet") && HandScript.HandsGone == 2)
        {
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
            EyeAnim.SetTrigger("AngryToWorry");
        }
    }

    void Update()
    {

        if (IntroDone == true)
        {

            TSpace += Time.deltaTime;

            if (TSpace >= 3f && Shooting == false)
            {
                State = Random.Range(MinState, MaxState);
                TSpace = 0;
            }

            if (Health <= 0f)
            {
                State = 0;
                Anim.SetTrigger("Die");
            }

            switch (State)
            {
                default:
                    break;
                case 1:
                    Anim.SetTrigger("Rock");
                    State = 0;
                    break;
                case 2:
                    Anim.SetTrigger("Hell");
                    State = 0;
                    break;
                case 3:
                    Anim.SetTrigger("Laser");
                    State = 0;
                    break;
                case 4:
                    Anim.SetTrigger("Spore");
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
                if (i == 0 && handscripts[0].DoneHands == false)
                {
                    BodyParts[i].position = new Vector3(SineAmp * Mathf.Sin(SineTime * SineFreq - SineOffset) * 1.5f, SineAmp * Mathf.Cos(SineTime * SineFreq - SineOffset) * 0.75f, 0f);
                    BodyParts[i].Rotate(0, 0, BodyParts[i].position.y);
                }
                
                if (i == 1 && handscripts[1].DoneHands == false)
                {
                    BodyParts[i].position = new Vector3(SineAmp * Mathf.Sin(SineTime * SineFreq + SineOffset) * -1.5f, SineAmp * Mathf.Cos(SineTime * SineFreq + SineOffset) * -.75f, 0f);
                    BodyParts[i].Rotate(0, 0, BodyParts[i].position.y * -1f);
                }

            }
            if (HandScript.HandsGone == 1)
            {
                EyeAnim.SetTrigger("ToAngry");
            }
            else if (HandScript.HandsGone == 2)
            {
                EyeAnim.SetTrigger("AngryToWorry");
            }

        }

        switch (HandScript.HandsGone)
        {
            case 0:
                BulletAmount = 8;
                MaxState = 3;
                MinState = 0;
                break;
            case 1:
                BulletAmount = 10;
                MaxState = 4;
                break;
            case 2:
                BulletAmount = 12;
                MaxState = 5;
                MinState = 1;
                break;
        }

        if (Shooting == true)
        {
            switch (ShootingType)
            {
                default:
                    break;
                case 0:
                    if (j >= 6)
                    {

                        Shooting = false;
                        j = 0;
                    }

                    TimingSpaceRock += Time.deltaTime;

                    if (TimingSpaceRock > 0.1f)
                    {
                        BulletAmount -= j;
                        BulletName = "Rock";
                        FindObjectOfType<AudioManager>().Play("Missle");
                        ShootCircle(BulletAmount, BulletName, BodyParts[j % 2], j + (HandScript.HandsGone * 29f));
                        TimingSpaceRock = 0;
                        j++;
                    }
                    break;
                case 1:

                    TimingSpaceRock += Time.deltaTime;

                    if (TimingSpaceRock > 0.1f)
                    {
                        BulletName = "SmallRock";
                        FindObjectOfType<AudioManager>().Play("Tail");
                        ShootCircle(BulletAmount, BulletName, BodyParts[4], 15f * Mathf.Sin(j * 0.125f) + (BulletAmount * 0.125f + j));
                        TimingSpaceRock = 0;
                        j++;
                    }
                    break;
            }

        }

    }

    //Functions

    public void ShootCircle(int BulletAmount, string BulletName, Transform tf, float Offset)
    {
        angle = Offset;
        for (int i = 0; i < BulletAmount; i++)
        {
            float AngleStep = 360f / BulletAmount;
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
        }
        
    }

    public void DeathSoundPlay()
    {
        FindObjectOfType<AudioManager>().Play("Boss die 2");
        StartCoroutine(camerashake.Shake(4f, 0.1f));
    }

    public void FadeBossTheme()
    {
        StartCoroutine(FindObjectOfType<AudioManager>().FadeAudio("Corrupt deity",0.33f));
        paralaxstuff.paraspeedGoal = 0f;
    }

    public void ShootSpore(int Amount)
    {
        FindObjectOfType<AudioManager>().Play("Thud");
        for (int i = 0; i < Amount; i++)
        {
            objectPooler.SpawnFromPool("SporeBomb", BodyParts[4].position, Quaternion.identity);
            StartCoroutine(camerashake.Shake(1f, 0.05f));
        }
    }

    public void StartLazer()
    { 
        objectPooler.SpawnFromPool("Lazer", BodyParts[4].position, Quaternion.identity);
        StartCoroutine(camerashake.Shake(5f, 0.015f));
    }

    public void StartMusic()
    {
        FindObjectOfType<AudioManager>().SetVolume("Corrupt deity", 1f);
        FindObjectOfType<AudioManager>().Play("Corrupt deity");
        paralaxstuff.paraspeedGoal = 100f;
    }

    public void Shoot()
    {
        Shooting = true;
        ShootingType = 0;
    }
    
    public void ShootEnd()
    {
        Shooting = false;
        j = 0;
    }

    public void ShootCenter()
    {
        Shooting = true;
        ShootingType = 1;
    }

    public void IntroComplete()
    {
        IntroDone = true;
    }

}
