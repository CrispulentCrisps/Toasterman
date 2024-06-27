using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;
    
    public Transform[] BodyPieces;
    Transform Target;
    public Transform[] MovingPieces;
    Vector2[] Velocities;

    public Rigidbody2D Head;
    
    public Animator Anim;
    
    public float Speed;
    public float RotSpeed;
    public int Health;
    float t = 0;
    float t2 = 0;
    
    public bool Firing = false;
    bool ShootPieces = false;
    
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Health > 0f)
        {
            //Movement
            Head.velocity = -BodyPieces[0].right * Speed;

            Vector2 Direction = Target.position - BodyPieces[0].position;

            Direction.Normalize();

            float RotateAmount = Vector3.Cross(Direction, BodyPieces[0].right).z;

            Head.angularVelocity = RotateAmount * RotSpeed;

            //Shooting
            t2 += Time.deltaTime;
            if (t2 > 10f)
            {
                Firing = false;
                t2 = 0;
            }
            else if (t2 > 5f)
            {
                Firing = true;
            }

            if (Firing)
            {
                t += Time.deltaTime;
                if (t >= 0.05f)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        objectPooler.SpawnFromPool("FlamethrowerBullet", BodyPieces[1].position, Quaternion.Euler(0f, 0f, BodyPieces[2].eulerAngles.z - 180));
                        t = 0;
                    }
                }
                Anim.SetTrigger("Fire");
            }
            else
            {
                Anim.SetTrigger("Stop");
            }
        }
        else
        {
            t = 0;
            t2 = 0;
            Anim.Play("Die");

            if (ShootPieces == false)
            {
                AudioManager.instance.Play("MediumExplosion");
                Head.angularVelocity = 0;
                Velocities = new Vector2[MovingPieces.Length];
                for (int i = 0; i < MovingPieces.Length; i++)
                {
                    objectPooler.SpawnFromPool("LavaSpurt", MovingPieces[i].position, Quaternion.identity);
                    Velocities[i] = new Vector2(Random.Range(-20, 20), Random.Range(1, 10));
                }
                EnemyScript.EnemyAmount--;
                ShootPieces = true;
            }

            for (int i = 0; i < MovingPieces.Length; i++)
            {
                Velocities[i] -= new Vector2(0f, 9.81f) * Time.deltaTime;
                MovingPieces[i].position += (Vector3)Velocities[i] * Time.deltaTime;
                float Rot = MovingPieces[i].rotation.z + (Velocities[i].x * 720f);
                MovingPieces[i].Rotate(new Vector3(0f, 0f, Rot));
            }
        }

    }
}
