using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrapEvents : MonoBehaviour, IPooledObject
{
    public Slider HealthSlider;
    private BossUIFunctions BossUi;

    public GameObject[] RemovableLayers;
    public GameObject[] Colliders;

    ObjectPools objectPooler;
    Dialog dialog;

    public ParalaxStuff ps;
    public Transform LaserPoint;
    public Transform tf;
    public Transform Target;
    public Transform AttackTf;
    public Transform AnimTf;
    public Transform CenterTf;
    public Transform[] ExlodedPartsTf;

    public AnimationCurve BGCurve;
    public Animator GunAnim;
    public Animator GunAnim2;
    public Animator LightAnim;
    
    public Transform TailTf;
    private Vector2 MovementTail;

    public Transform Seg1Trans;
    public Transform Seg2Trans;
    public Transform Seg3Trans;

    bool Seg1Shot = false;
    bool Seg2Shot = false;
    bool Seg3Shot = false;

    bool SegGrav = false;
    public bool StartSpawning = false;

    public Vector3 STrans;

    private Vector2 MovementSeg1;
    private Vector2 MovementSeg2;
    private Vector2 MovementSeg3;
    private Vector3[] ExplodedPartsMovement;

    private bool TailShot;
    private bool GravityOn;
    private bool Exploding = false;
    private bool Exploding2 = false;
    private bool Ended = false;
    public bool firing = false;

    public bool IsAttacking = true;
    float Amp = 2;
    float T = 0;
    float T2 = 0;

    public int State;

    public void Start()
    {
        State = 0;
        MovementTail = new Vector2(0, 0);
        objectPooler = ObjectPools.Instance;
        LightAnim = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Animator>();
        HealthSlider = GameObject.FindGameObjectWithTag("BossUI").GetComponent<Slider>();
        ps = GameObject.FindGameObjectWithTag("BackGroundStuff").GetComponent<ParalaxStuff>();
        dialog = GameObject.FindGameObjectWithTag("dialog").GetComponent<Dialog>();
    }

    public void OnObjectSpawn()
    {
        StartCoroutine(AudioManager.instance.FadeAudio("ScrapyardTheme", 1f));
        transform.position = new Vector3(-13, -9f, 0f);
        transform.Rotate(0, 180, 0);
        State = 0;
        MovementTail = new Vector2(0, 0);
        LightAnim = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Animator>();
        HealthSlider = GameObject.FindGameObjectWithTag("BossUI").GetComponent<Slider>();
        ps = GameObject.FindGameObjectWithTag("BackGroundStuff").GetComponent<ParalaxStuff>();
        dialog = GameObject.FindGameObjectWithTag("dialog").GetComponent<Dialog>();
    }

    public void Update()
    {
        T += Time.deltaTime;
        T2 += Time.deltaTime;

        if (Ended)
        {   
            for (int i = 0; i < ExlodedPartsTf.Length; i++)
            {
                ExplodedPartsMovement[i].y -= 9.81f * Time.deltaTime;
                ExlodedPartsTf[i].Translate(ExplodedPartsMovement[i] * Time.deltaTime);
            }
        }

        if (Exploding)
        {
            if (T2 >= 0.1f)
            {
                ObjectPools.Instance.SpawnFromPool("Boom", new Vector3(CenterTf.position.x + Random.RandomRange(-10f, 10f) - 15f, CenterTf.position.y + Random.RandomRange(-5f, 5f) + 5f, -2f), Quaternion.identity);
                T2 = 0;
            }
        }

        if (Exploding2)
        {
            if (T >= .75f)
            {
                AudioManager.instance.Play("MediumExplosion");
                ObjectPools.Instance.SpawnFromPool("MassiveExplosion", new Vector3(CenterTf.position.x + Random.RandomRange(-10f, 10f) - 15f, CenterTf.position.y + Random.RandomRange(-5f, 5f) + 5f, CenterTf.position.z + Random.RandomRange(-2f, 2f)), Quaternion.identity);
                T = 0;
            }
        }

        if (TailShot){
            MovementTail = new Vector2(-6f, 9.81f);
            TailShot = false;
            GravityOn = true;
        }
		if (GravityOn)
		{
            MovementTail -= new Vector2(-12f, 18.36f) * Time.deltaTime;
		}

        if (Seg1Shot)
        {
            GravityOn = false;
            MovementTail = new Vector2(3, 0);
            SegGrav = true;
            Seg1Shot = false;
        }

        if (SegGrav)
        {
            MovementSeg1 += new Vector2(0, 18.36f) * Time.deltaTime;
            Seg1Trans.Translate(MovementSeg1 * Time.deltaTime);
            if (Seg2Shot)
            {
                MovementSeg2 += new Vector2(0, 18.36f) * Time.deltaTime;
                Seg2Trans.Translate(MovementSeg2 * Time.deltaTime);
            }
            if (Seg3Shot)
            {
                MovementSeg3 += new Vector2(0, 18.36f) * Time.deltaTime;
                Seg3Trans.Translate(MovementSeg3 * Time.deltaTime);
            }
        }

        Target.position = new Vector3(Target.position.x, AttackTf.position.y + (Amp * 0.5f * Mathf.Sin(T * 12f)), Target.position.z);

        if (ScrapBossAI.health > 0f)
        {
            HealthSlider.value = ScrapBossAI.health;
        }
        else
        {
            BossUi.Closing();
        }

        if (IsAttacking)
        {
            if (Amp > 0)
            {
                Amp -= 2 * Time.deltaTime;
            }
            else if (Amp < 0)
            {
                Amp = 0;
            }
        }
        else
        {
            if (Amp < 2)
            {
                Amp += 2 * Time.deltaTime;
            }
            else if (Amp < 0)
            {
                Amp = 0;
            }
        }

        Mathf.Clamp(AttackTf.position.x, 1.5f, 3f);
        TailTf.Translate(MovementTail * Time.deltaTime);
    }

    void StartMusic()
    {
        AudioManager.instance.Play("ScrapBossTheme");
        AudioManager.instance.Stop("ScrapyardTheme");
    }

    public void RemoveTailColliders()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].SetActive(false);
        }
    }

    public void ChangeLighting()
    {
        LightAnim.SetTrigger("ChangeLight");
    }
    public void Explode()
    {
        ExplodedPartsMovement = new[] {
            new Vector3(0f,-2f,-4f),
            new Vector3(0f,-2f,-4f),
            new Vector3(0f,-8f,-12f),
            new Vector3(0f,-8f,-12f),
            new Vector3(0f,-12f,-18f),
            new Vector3(0f,-12f,-18f)
        };
        Ended = true;
    }

    public void LastBoom()
    {
        for (int i = 0; i < 5; i++)
        {
            ObjectPools.Instance.SpawnFromPool("MassiveExplosion", new Vector3(CenterTf.position.x + Random.RandomRange(-10f, 10f) - 15f, CenterTf.position.y + Random.RandomRange(-5f, 5f) + 5f, -Random.RandomRange(-2f, 2f)), Quaternion.identity);
        }
        AudioManager.instance.Play("MassiveExplosion");
    }
    public void StartBGChange()
    {
        StartCoroutine(ps.MoveYAToB(.3f,new float[] { -13f, -13f, -14f, -14f, -12.5f, -12.5f, -17f, -17f, -17f, -17f, -17f, -17f, -19f, -19f, 0f, 0f, -13, -13, -13, -13 }, BGCurve));
    }
    public void ChangeLayers()
    {
        for (int i = 0; i < RemovableLayers.Length; i++)
        {
            RemovableLayers[i].SetActive(false);
        }
    }
    public void SetState(int state)
    {
        State = state;
    }

    void StartAmp()
    {
        ScrapBossAI.StartAmp = true;
    }
    public void StartExploding()
    {
        Exploding = true;
    }
    public void StopExploding()
    {
        Exploding = false;
    }

    public void StartExploding2()
    {
        Exploding2 = true;
    }
    public void StopExploding2()
    {
        Exploding2 = false;
    }

    public void P4Spawn()
    {
        StartSpawning = true;
    }
    public void IsFiring()
    {
        firing = true;
    }

    public void Shoot5Way()
    {
        StartCoroutine(BulletPatternsModule.ShootArcEnum(120f, 5, "SideLaser", LaserPoint, 110f, 0.02f));
    }
    public void ShootGun()
	{
        GunAnim.Play("GunShoot");
    }
    public void ShootGun2()
    {
        GunAnim2.Play("GunShoot2");
    }
    public void ShootSeg1()
    {
        IsAttacking = false;
        Seg1Shot = true;
        MovementTail = new Vector2(3,0);
        AudioManager.instance.Play("ScrapPartBroke");
    }
    public void ShootTail()
    {
        TailShot = true;
        MovementSeg1 = new Vector2(6, 10);
        AudioManager.instance.Play("ScrapPartBroke");
    }
    public void ShootSeg2()
    {
        Seg2Shot = true;
        MovementSeg2 = new Vector2(0, 6);
        SegGrav = true;
        AudioManager.instance.Play("ScrapPartBroke");
    }
    public void ShootSeg3()
    {
        Seg3Shot = true;
        MovementSeg3 = new Vector2(0, 6);
        SegGrav = true;
        AudioManager.instance.Play("ScrapPartBroke");
    }
    public void FollowPlayer()
    {
        IsAttacking = false;
    }
    public void UnFollowPlayer()
    {
        IsAttacking = true;
    }

    public void CurrentPos()
    {
        STrans = AnimTf.position;
    }

    public void FlashBang()
    {
        objectPooler.SpawnFromPool("FlashBang", new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    public void SoundRoar()
    {
        AudioManager.instance.Play("PurityRoar1");
    }

    public void OpenHealthbar()
    {
        HealthSlider = GameObject.FindGameObjectWithTag("BossUI").GetComponent<Slider>();
        BossUi = HealthSlider.GetComponent<BossUIFunctions>();
        HealthSlider.maxValue = ScrapBossAI.health;
        BossUi.Opening();
    }

    public void StopAndSlowDown() {

        ps.paraspeedGoal = 5f;
        StartCoroutine(AudioManager.instance.FadeAudio("ScrapBossTheme", .75f));
    }

    public void OpenDialog()
    {
        PlanetTally pt = GameObject.FindGameObjectWithTag("TallyCounter").GetComponent<PlanetTally>();

        AudioManager.instance.Play("Victory2");
        PlanetTally.PlanetsDone[2] = true;
        PlanetTally.TimesCompleted[2]++;
        if (Shooting.TargetScore > PlanetTally.PlanetScore[2])
        {
            PlanetTally.PlanetScore[2] = Shooting.TargetScore;
        }
        pt.SaveData();

        AudioManager.instance.Stop("ScrapBossTheme");
        StartCoroutine(dialog.BoxIn(1f));
    }
}
