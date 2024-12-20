﻿using UnityEngine;
using UnityEngine.UI;

public class AncientAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Slider HealthSlider;
    private BossUIFunctions BossUi;

    public GameObject BG;

    public Animator Anim;
    public Animator EyeAnim;
    public Animator ShroomAnim;

    public ParticleSystem ps;

    public CameraShake camerashake;
    public BigMushroom bigmushroom;
    public ParalaxStuff paralaxstuff;
    public HandScript[] handscripts;

    public GameObject[] Hands;

    public Transform[] BodyParts;

    private string BulletName;

    public int State = 0;
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
    private bool HealthShown = false;
    public bool IntroDone = false;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        SineFreq = .33f;
        SineAmp = 1f;
        j = 0;
        handscripts[0] = Hands[0].GetComponent<HandScript>();
        handscripts[1] = Hands[1].GetComponent<HandScript>();
        gameObject.transform.position = new Vector3(0f, 0f, -5f);
    }

    public void OnObjectSpawn()
    {
        gameObject.transform.Rotate(0f, -90f, 0f);
        gameObject.transform.position = new Vector3(0f,0f,-5f);
        camerashake = Camera.main.GetComponent<CameraShake>();
        BG = GameObject.Find("BG stuff");
        paralaxstuff = BG.GetComponent<ParalaxStuff>();
        SineFreq = 1f;
        SineAmp = 1f;
        j = 0;
        ShroomAnim = GameObject.Find("BigMushroom").GetComponent<Animator>();
        bigmushroom = GameObject.Find("BigMushroom").GetComponent<BigMushroom>();
        StartCoroutine(AudioManager.instance.FadeAudio("How shroomy are you?", 1f));
        ps.enableEmission = false;
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
        var emmision = ps.emission;
        emmision.rateOverTime = Mathf.Clamp((200 / (Health * 0.05f)) - 1,0f,200f);
        if (HealthShown)
        {
            HealthSlider.value = handscripts[0].Health + handscripts[1].Health + Health;
        }

        if (IntroDone == true)
        {

            TSpace += Time.deltaTime;

            if (TSpace >= 3f && Shooting == false)
            {
                int StateCheck = State;
                State = Random.Range(MinState, MaxState);
                if (StateCheck == State)
                {
                    State = Random.Range(MinState, MaxState);
                }
                TSpace = 0;
            }

            if (Health <= 0f)
            {
                BossUi.Closing();
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
                case 5:
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
                ps.enableEmission = true;
            }

        }

        switch (HandScript.HandsGone)
        {
            case 0:
                BulletAmount = 4;
                MaxState = 3;
                MinState = 0;
                break;
            case 1:
                BulletAmount = 6;
                MaxState = 4;
                break;
            case 2:
                BulletAmount = 8;
                MaxState = 6;
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
                        AudioManager.instance.Play("Missle");
                        BulletPatternsModule.ShootArc(360f, BulletAmount, BulletName, BodyParts[j % 2], j + (HandScript.HandsGone * 29f));
                        TimingSpaceRock = 0;
                        j++;
                    }
                    break;
                case 1:
                    TimingSpaceRock += Time.deltaTime;
                    if (TimingSpaceRock > 0.1f)
                    {
                        BulletName = "SmallRock";
                        AudioManager.instance.Play("Tail");
                        BulletPatternsModule.ShootArc(360f, BulletAmount, BulletName, BodyParts[2], 10f * Mathf.Sin(j * 0.25f) + (BulletAmount * 0.125f + j));
                        TimingSpaceRock = 0;
                        j++;
                    }
                    break;
            }
        }
    }

    //Functions
    public void DeathSoundPlay()
    {
        AudioManager.instance.Play("Boss die 2");
        StartCoroutine(camerashake.Shake(4f, 0.1f));
    }

    public void FadeBossTheme()
    {
        StartCoroutine(AudioManager.instance.FadeAudio("Corrupt deity",0.33f));
        paralaxstuff.paraspeedGoal = 0f;
    }

    public void ShootSpore(int Amount)
    {
        AudioManager.instance.Play("Thud");
        for (int i = 0; i < Amount; i++)
        {
            objectPooler.SpawnFromPool("SporeBomb", BodyParts[2].position, Quaternion.identity);
            StartCoroutine(camerashake.Shake(1f, 0.05f));
        }
    }

    public void StartLazer()
    { 
        objectPooler.SpawnFromPool("Lazer", BodyParts[2].position, Quaternion.identity);
        StartCoroutine(camerashake.Shake(5f, 0.00015f));
    }

    public void StartMusic()
    {
        OpenHealthbar();
        HealthSlider.maxValue = handscripts[0].Health + handscripts[1].Health + Health;
        AudioManager.instance.SetVolume("Corrupt deity", 1f);
        AudioManager.instance.Play("Corrupt deity");
        paralaxstuff.paraspeedGoal = 50f;
    }

    public void Shoot()
    {
        ShootingType = 0;
        Shooting = true;
    }
    
    public void ShootEnd()
    {
        Shooting = false;
        j = 0;
    }

    public void ShootCenter()
    {
        ShootingType = 1;
        Shooting = true;
    }

    public void IntroComplete()
    {
        IntroDone = true;
    }

    public void KillShroom()
    {
        ShroomAnim.SetTrigger("Die");
    }

    void OpenHealthbar()
    {
        HealthSlider = GameObject.FindGameObjectWithTag("BossUI").GetComponent<Slider>();
        BossUi = HealthSlider.GetComponent<BossUIFunctions>();
        HealthSlider.maxValue = Health;
        BossUi.Opening();
        HealthShown = true;
    }
}
