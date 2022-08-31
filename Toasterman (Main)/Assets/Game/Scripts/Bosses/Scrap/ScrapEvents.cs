using UnityEngine;
using System.Collections;

public class ScrapEvents : MonoBehaviour
{
    public GameObject[] RemovableLayers;
    public GameObject[] Colliders;

    public ParalaxStuff ps;
    public Transform LaserPoint;
    public Transform tf;
    public Transform Target;
    public Transform AttackTf;
    public Transform AnimTf;

    public AnimationCurve BGCurve;
    public Animator GunAnim;
    public Animator GunAnim2;
    
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

    private bool TailShot;
    private bool GravityOn;

    public bool IsAttacking = true;
    float Amp = 2;
    float T = 0;

    public int State;

    public void Start()
    {
        State = 0;
        MovementTail = new Vector2(0, 0);
    }

    public void Update()
    {
        T += Time.deltaTime;

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
                Seg2Trans.Translate(MovementSeg3 * Time.deltaTime);
            }
        }

        Target.position = new Vector3(Target.position.x, AttackTf.position.y + (Amp * 0.5f * Mathf.Sin(T * 12f)), Target.position.z);

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

    public void RemoveTailColliders()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].SetActive(false);
        }
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

    public void P4Spawn()
    {
        StartSpawning = true;
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
    }
    public void ShootTail()
    {
        TailShot = true;
        MovementSeg1 = new Vector2(6, 10);
    }
    public void ShootSeg2()
    {
        Seg2Shot = true;
        MovementSeg2 = new Vector2(0, 6);
        SegGrav = true;
    }
    public void ShootSeg3()
    {
        Seg3Shot = true;
        MovementSeg3 = new Vector2(0, 6);
        SegGrav = true;
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
}
