using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapGun : MonoBehaviour, IPooledObject
{
    public GameObject LaserPoinyer;

    ObjectPools objectPooler;

    [SerializeField] private GameObject LaserLeft;
    [SerializeField] private GameObject LaserRight;
    [SerializeField] private Transform Target;
    [SerializeField] private float Range;
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float MinVel;
    [SerializeField] private float MaxVel;
    [SerializeField] private int BulletAmount;
    [SerializeField] private string BulletName;
    [SerializeField] private Vector2[] Points;
    [SerializeField] private int Index = 0;
    [SerializeField] private int dir;
    [SerializeField] private bool Fired;
    public void OnObjectSpawn()
    {
        Target = GameObject.FindWithTag("Player").transform;
        Fired = false;
        LaserLeft.active = false;
        LaserRight.active = false;
        dir = Random.RandomRange(0, 2);
        Index = 0;
        Range = Random.RandomRange(-10, 10);
        transform.position = new Vector2(0f, Random.RandomRange(-7f, 7f));
        Points = new Vector2[4];
        if (dir == 0)
        {
            Points[0] = transform.position;
            Points[1] = new Vector2(transform.position.x - 2, transform.position.y);
            Points[2] = new Vector2(transform.position.x - 2, transform.position.y + Range);
            Points[3] = new Vector2(transform.position.x, transform.position.y + Range);
        }
        else
        {
            Points[0] = transform.position;
            Points[1] = new Vector2(transform.position.x + 2, transform.position.y);
            Points[2] = new Vector2(transform.position.x + 2, transform.position.y + Range);
            Points[3] = new Vector2(transform.position.x, transform.position.y + Range);
        }
        Speed = Random.RandomRange(3f, 8f);
    }

    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        Fired = false;
        LaserLeft.active = false;
        LaserRight.active = false;
        dir = Random.RandomRange(0, 2);
        Index = 0;
        Range = Random.RandomRange(-10, 10);
        transform.position = new Vector2(0f, Random.RandomRange(-7f, 7f));
        Points = new Vector2[4];
        if (dir == 0)
        {
            Points[0] = transform.position;
            Points[1] = new Vector2(transform.position.x - 2, transform.position.y);
            Points[2] = new Vector2(transform.position.x - 2, transform.position.y + Range);
            Points[3] = new Vector2(transform.position.x, transform.position.y + Range);
        }
        else
        {
            Points[0] = transform.position;
            Points[1] = new Vector2(transform.position.x + 2, transform.position.y);
            Points[2] = new Vector2(transform.position.x + 2, transform.position.y + Range);
            Points[3] = new Vector2(transform.position.x, transform.position.y + Range);
        }
        Speed = Random.RandomRange(3f, 8f);
    }

    void FixedUpdate()
    {
        if (Index < 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[Index], Speed * Time.deltaTime);
        }

        if (Index > 0 && Index < 2)
        {
            if (dir == 0)
            {
                LaserLeft.active = true;
                LaserRight.active = false;
            }
            else
            {
                LaserLeft.active = false;
                LaserRight.active = true;
            }
        }

        if (!Fired)
        {
            if (dir == 0)
            {
                if (Target.position.y > transform.position.y + 1 && Target.position.y < transform.position.y - 1 && Target.position.x < transform.position.x)
                {
                    BulletPatternsModule.ShootLine(BulletSpeed, MinVel, MaxVel, BulletAmount, BulletName, transform, 90f);
                    Fired = true;
                    LaserLeft.active = false;
                    LaserRight.active = false;
                }
            }
            else
            {
                if (Target.position.y > transform.position.y + 1 && Target.position.y < transform.position.y - 1 && Target.position.x < transform.position.x)
                {
                    BulletPatternsModule.ShootLine(BulletSpeed, MinVel, MaxVel, BulletAmount, BulletName, transform, 270f);
                    Fired = true;
                    LaserLeft.active = false;
                    LaserRight.active = false;
                }
            }
        }

        if ((Vector2)transform.position == Points[Index] && Index < Points.Length)
        {
            Index++;
        }
        else if ((Vector2)transform.position == Points[Index] && Index >= Points.Length-1)
        {
            gameObject.active = false;
        }
    }
}
