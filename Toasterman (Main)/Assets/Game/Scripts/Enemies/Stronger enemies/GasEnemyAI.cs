using UnityEngine;

public class GasEnemyAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform[] SpritePos;//First transform is the players, Second is for the head spitting spores, 3rd is for the main Transform (aka prefab Transform)

    public SpriteRenderer[] SRends;

    public Animator Anim;

    private Vector2 CurrentVel;

    private float[] StrandHealth;

    private float XVel;
    private float YVel;
    private float WaitPeriod;
    float height;
    float width;
    [Range(1f,25f)]
    public float MaxVel;

    public int BulletAmount;

    public bool Shooting = false;
    public bool IsLeft = true;

    void Start()
    {
        GameObject Ship = GameObject.Find("Ship");//gets the game object
        SpritePos[0] = Ship.GetComponent<Transform>();// gets the Transform 
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        SpritePos[2].position = new Vector3(0f, height + 3f, 0f);
        YVel = MaxVel * 20f;
        XVel = MaxVel * 5f;
        WaitPeriod = 0f;
    }

    public void OnObjectSpawn()
    {
        GameObject Ship = GameObject.Find("Ship");//gets the game object
        SpritePos[0] = Ship.GetComponent<Transform>();// gets the Transform 
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        SpritePos[2].position = new Vector3(0f, height + 3f,0f);
        YVel = MaxVel * 20f;
        XVel = MaxVel * 5f;
        WaitPeriod = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        WaitPeriod += Time.deltaTime;

        if (WaitPeriod >= 7.5f)
        {
            WaitPeriod = 0f;
            CurrentVel.x = 0f;
            IsLeft = !IsLeft;
        }
        else if (WaitPeriod % 1f >= 1f)
        {
            Anim.SetTrigger("Shoot");
        }

        if (SpritePos[2].position.y > SpritePos[0].position.y)
        {
            CurrentVel.y -= YVel * Time.deltaTime;
        }
        else if (SpritePos[2].position.y < SpritePos[0].position.y)
        {
            CurrentVel.y += YVel * Time.deltaTime;
        }

        if (IsLeft == true)
        {
            CurrentVel.x -= XVel * Time.deltaTime;
        }
        else if (IsLeft == false)
        {
            CurrentVel.x += XVel * Time.deltaTime;
        }

        if (SpritePos[2].position.x > SpritePos[0].position.x)
        {
            for (int i = 0; i < SRends.Length; i++)
            {
                SRends[i].flipX = false;
            }
        }
        else if (SpritePos[2].position.x < SpritePos[0].position.x)
        {
            for (int i = 0; i < SRends.Length; i++)
            {
                SRends[i].flipX = true;
            }
        }

        if (YVel >= MaxVel)
        {
            YVel = MaxVel;
        }
        else if (YVel <= -MaxVel)
        {
            YVel = -MaxVel;
        }
        
        if (XVel >= MaxVel)
        {
            XVel = MaxVel;
        }
        else if (XVel <= -MaxVel)
        {
            XVel = -MaxVel;
        }
    }

    void FixedUpdate()
    {
        SpritePos[2].Translate(CurrentVel * Time.deltaTime);
    }

    public void ShootBullet(int BulletAmount, string BulletName)
    {
        float RegularAngle = 0;
        float Step = (-RegularAngle / 2) + 135;
        Quaternion BulletRot = Quaternion.Euler(0, 0, 0); ;
        for (int i = 0; i < BulletAmount; i++)
        {
            Step -= RegularAngle / BulletAmount;
            BulletRot = Quaternion.Euler(0, 0, Step % 360);
            objectPooler.SpawnFromPool(BulletName, SpritePos[1].position, BulletRot);
        }

    }
}
