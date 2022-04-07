using UnityEngine;

public class ScrapEvents : MonoBehaviour
{
    public GameObject[] RemovableLayers;
    public ParalaxStuff ps;
    public Transform LaserPoint;
    public AnimationCurve BGCurve;
    
    public Transform TailTf;
    private Vector2 MovementTail;
    private bool TailShot;

    public static int State;
    public void Start()
    {
        State = 0;
        MovementTail = new Vector2(0, 0);
    }

    public void Update()
    {
        if (TailShot){
            MovementTail = new Vector2(MovementTail.x - 6f, MovementTail.y - 9.81f) * Time.deltaTime;
        }
        TailTf.Translate(MovementTail * Time.deltaTime);
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

    public void ShootTail()
	{
        TailShot = true;
        MovementTail = new Vector2(6, 10);
	}
}
