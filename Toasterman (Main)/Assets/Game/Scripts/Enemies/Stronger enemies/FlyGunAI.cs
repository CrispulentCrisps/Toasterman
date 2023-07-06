using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyGunAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public GameObject pointer;

    public Transform tf;
    public Transform LightTf;
    public Transform[] BodyPieces;

    Transform Target;
    
    public SpriteRenderer FaceSr;
    public SpriteRenderer[] CollidePartSr;

    public BoxCollider2D coll;

    public Animator Anim;

    public Color[] Colours;

    public Sprite[] Expressions;

    Vector2 Movement;
    Vector2[] BodyMovement = { new Vector2(0f,0f), new Vector2(0f, 0f) };

    public float TimeToShoot;
    public float Health;

    bool IsDead = false;
    bool IsMoving = true;
    bool HasFired = false;
    bool Charging = false;
    void Start()
    {
        objectPooler = ObjectPools.Instance;
        tf.position = new Vector3(-20f, 10f, 0f);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        tf.position = new Vector3(-20f,15f,0f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Health -= coll.GetComponent<DamageScript>().Damage;
            for (int i = 0; i < CollidePartSr.Length; i++)
            {
                CollidePartSr[i].color = Colours[0];
            }
            if (Health <= 0f)
            {
                if (!IsDead)
                {
                    Shooting.TargetScore += this.GetComponent<DamageScript>().Points * this.GetComponent<DamageScript>().PointMultiplier;
                    Anim.Play("FlyGunDie");
                    BodyMovement[0] = new Vector2(0f, -9.81f) * .25f;
                    BodyMovement[1] = new Vector2(0f, 9.81f) * .25f;
                    coll.enabled = false;
                }
                IsDead = true;
            }
        }
    }

    void Update()
    {
        if (IsDead)
        {
            IsMoving = false;   
            TimeToShoot = 99f;
            BodyMovement[0] -= new Vector2(0f, -9.81f) * Time.deltaTime;
            BodyMovement[1] -= new Vector2(0f, 9.81f) * Time.deltaTime;
            for (int i = 0; i < BodyMovement.Length; i++)
            {
                BodyPieces[i].Translate(BodyMovement[i] * Time.deltaTime);
            }
        }

        for (int i = 0; i < CollidePartSr.Length; i++)
        {
            CollidePartSr[i].color += Colours[1] * Time.deltaTime * 10f;
        }
        tf.Translate(Movement * Time.deltaTime);
        TimeToShoot -= Time.deltaTime;
        if (TimeToShoot <= 0f && !HasFired)
        {
            Fire();
            AudioManager.instance.Play("LaserDischarge");
        }

        if (TimeToShoot <= .5f)
        {
            if (!Charging)
            {
                AudioManager.instance.Play("LaserCharge");
                Charging = true;
            }
            LightTf.position = Vector3.MoveTowards(LightTf.position, new Vector3(-1f, 0f, 0f), 10f * Time.deltaTime);
            Movement.y = 0f;
            IsMoving = false;
            pointer.active = false;
            FaceSr.sprite = Expressions[1];
        }

        if (IsMoving)
        {
            if (tf.position.y < Target.position.y + 3f)
            {
                Movement.y = 5;
            }
            else if (tf.position.y > Target.position.y + 3f)
            {
                Movement.y = -5;
            }
            if (tf.position.x < 10f)
            {
                Movement.x = 20f;
            }
            else
            {
                Movement.x = 0f;
            }
            FaceSr.sprite = Expressions[0];
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        EnemyScript.EnemyAmount--;
    }

    public void Fire()
    {
        EnemyScript.EnemyAmount--;
        HasFired = true;
        objectPooler.SpawnFromPool("FlyLaser", new Vector3(tf.position.x + 40f, tf.position.y - 3f, 0f), Quaternion.identity);
        Movement.x = 60f;
    }
}
