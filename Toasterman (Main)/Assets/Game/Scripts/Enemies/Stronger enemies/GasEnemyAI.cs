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

    public float Health;

    float height;
    float width;

    [Range(1f,25f)]
    public float MaxVel;

    [Range(1, 10)]
    public int BulletAmount;

    public bool Shooting = false;
    public bool IsLeft = true;

    void Start()
    {
        GameObject Ship = GameObject.Find("Ship");//gets the game object
        SpritePos[0] = Ship.GetComponent<Transform>();// gets the Transform 
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        SpritePos[2].position = new Vector3(0f, height + 3f, width + 3f);
        YVel = MaxVel * 1.5f;
        XVel = MaxVel * 1.5f;
        WaitPeriod = 0f;
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        GameObject Ship = GameObject.Find("Ship");//gets the game object
        SpritePos[0] = Ship.GetComponent<Transform>();// gets the Transform 
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        SpritePos[2].position = new Vector3(0f, height + 3f, width + 3f);
        YVel = MaxVel * 1.5f;
        XVel = MaxVel * 1.5f;
        WaitPeriod = 0f;
    }
    void OnTriggerEnter2D(Collider2D coll)
    { 
    

    
    }
        // Update is called once per frame
        void Update()
    {
        WaitPeriod += Time.deltaTime;

        if (WaitPeriod >= 7.5f)
        {
            WaitPeriod = 0f;
            CurrentVel.x = 0f;
        }
        if (WaitPeriod >= 2f)
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
        if (SpritePos[2].position.x > SpritePos[0].position.x)
        {
            CurrentVel.x -= XVel * Time.deltaTime;
        }
        else if (SpritePos[2].position.x < SpritePos[0].position.x)
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

        if (CurrentVel.y >= MaxVel)
        {
            CurrentVel.y = MaxVel;
        }
        else if (CurrentVel.y <= -MaxVel)
        {
            CurrentVel.y = -MaxVel;
        }

        if (CurrentVel.x >= MaxVel)
        {
            CurrentVel.x = MaxVel;
        }
        else if (CurrentVel.x <= -MaxVel)
        {
            CurrentVel.x = -MaxVel;
        }

        if (Shooting)
        {
            ShootBullet(BulletAmount, "Spore shot");
            Shooting = false;
        }
    }

    void FixedUpdate()
    {
        SpritePos[2].Translate(CurrentVel * Time.deltaTime);
    }

    public void ShootBullet(int BulletAmount, string BulletName)
    {
        float ArcSize = (360f / (float)BulletAmount) * 0.25f;
        float StartAngle = 0f + ArcSize;
        for (int i = 0; i < BulletAmount; i++)
        {
            StartAngle -= (ArcSize / BulletAmount) * 2f;
            Vector3 difference = SpritePos[1].position - SpritePos[0].position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion BulletRot = Quaternion.Euler(0.0f, 0.0f, rotationZ - StartAngle);
            objectPooler.SpawnFromPool(BulletName, SpritePos[1].position, BulletRot);
        }
    }
    public void Shoot()
    {
        Shooting = true;
    }
}
