using UnityEngine;

public class ScrapEvents : MonoBehaviour
{
    public GameObject[] RemovableLayers;
    public GameObject[] Colliders;

    public ParalaxStuff ps;
    public Transform LaserPoint;
    public Transform CannonPoint;
    public AnimationCurve BGCurve;
    public Animator GunAnim;
    
    public Transform TailTf;
    private Vector2 MovementTail;
    private bool TailShot;
    private bool GravityOn;

    public int State;
    public void Start()
    {
        State = 0;
        MovementTail = new Vector2(0, 0);
    }

    public void Update()
    {
        if (TailShot){
            MovementTail = new Vector2(-6f, 9.81f);
            TailShot = false;
            GravityOn = true;
        }
		if (GravityOn)
		{
            MovementTail -= new Vector2(-12f, 9.18f) * Time.deltaTime;
		}

        TailTf.Translate(MovementTail * Time.deltaTime);
    }

    public void RemoveTailColliders()
    {
        for (int i = 0; i < 4; i++)
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

    public void Shoot5Way()
    {
        StartCoroutine(BulletPatternsModule.ShootArcEnum(120f, 5, "SideLaser", LaserPoint, 110f, 0.02f));
    }

    public void ShootGun()
	{
        GunAnim.Play("GunShoot");
	}

    public void FireBullet()
    {
        BulletPatternsModule.ShootArc(0f, 1, "GunBomb", CannonPoint, 0f);
    }

    public void ShootTail()
	{
        TailShot = true;
        MovementTail = new Vector2(6, 10);
	}
}
