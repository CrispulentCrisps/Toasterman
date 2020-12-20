using UnityEngine;

public class GasEnemyAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform[] SpritePos;//First transform is the players, Second is for the head spitting spores, 3rd is for the main Transform (aka prefab Transform)

    public SpriteRenderer[] SRends;

    public Animator Anim;

    public Color HurtColour;

    private Vector2 CurrentVel;

    public string BulletName;

    private float XVel;
    private float YVel;
    private float WaitPeriod;
    [Range(1f, 25f)]
    public float MaxVel;

    public float Health;

    float height;
    float width;

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
        SpritePos[2].position = new Vector3(0f, height + 3f, width + 6f);
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
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;

            for (int i = 0; i < SRends.Length; i++)
            {
                SRends[i].color = HurtColour;
            }

            if (Health <= 0)
            {
                Anim.SetTrigger("Die");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaitPeriod += Time.deltaTime;
        for (int i = 0; i < SRends.Length; i++)
        {
            SRends[i].color += new Color(1f, 1f, 1f) * Time.deltaTime * 5f;
        }
        //Shooting and periodic stopping
        if (WaitPeriod >= 7.5f)
        {
            WaitPeriod = 0f;
            CurrentVel.x = 0f;
        }
        if (WaitPeriod >= 2f)
        {
            Anim.SetTrigger("Shoot");
        }
        //Movement
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
        //Flips sprites if left of player
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
        //Limits x and y speeds
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
            ShootBullet(BulletAmount, BulletName);
            Shooting = false;
        }
    }

    void FixedUpdate()
    {
        SpritePos[2].Translate(CurrentVel * Time.deltaTime);
    }

    public void ShootBullet(int BulletAmount, string BulletName)
    {
        Vector3 difference = SpritePos[1].position - SpritePos[0].position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        BulletPatternsModule.ShootArc(45f,BulletAmount,BulletName,SpritePos[1],rotationZ + 135f + (75f/BulletAmount));
    }

    public void Shoot()
    {
        Shooting = true;
    }

}
