using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;
    public CameraShake cameraShake;
    public Transform Tf;
    public Transform ShootPoint;
    public Transform Target;
    public Transform Head;
    public Transform[] Segments;
    public SpriteRenderer Sr;
    public Sprite[] Rotation;
    public float RiseSpeed;
    float[] XOffset;
    float[] XOffsetShoot;
    float[] Angles;
    float T;
    float T2;
    Vector2 MovementSpeed;
    public float ShakeAmp;
    public float ShakeFreq;
    public float Health;
    float Preamble;
    int HeightLevel;
    bool IsFlipped;
    bool Exploded;
    bool HasReached = false;
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        objectPooler = ObjectPools.Instance;
        XOffset = new float[] { -.03f, .2f, .5f, .75f, 1f};
        XOffsetShoot = new float[] {0f, -1f, -1.5f, -1.8f, -2.8f };
        Angles = new float[] { 0f, 22.5f, 30f, 45f, 60f};
        //.position = new Vector3(Random.RandomRange(-9f, 9f), -10f, Tf.position.z);
        Tf.position = new Vector3(Random.RandomRange(-9f, 9f), -10f, Tf.position.z);
        HeightLevel = Random.Range(-2, 2);
        Exploded = false;
    }


    public void OnObjectSpawn()
    {
        EnemyScript.EnemyAmount++;
        Tf.position = new Vector3(Random.RandomRange(-9f, 9f), -10f, Tf.position.z);
        HeightLevel = Random.Range(4, 12);
        Preamble = 0;
    }
     
    void Update()
    {
        if (!Exploded)
        {
            Preamble += Time.deltaTime;
            T += Time.deltaTime;
            T2 += Time.deltaTime;
            int index = 0;
            int Mult = 1;
            if (Health <= 0f)
            {
                Explode();
            }

            #region IndexDetermine
            if (Target.position.x >= Tf.position.x)
            {
                Mult = 1;
                IsFlipped = false;
            }
            else
            {
                Mult = -1;
                IsFlipped = true;
            }

            if (Mult == 1)
            {
                if (Target.position.x < Tf.position.x + 1 && Target.position.x > Tf.position.x - 1)
                {
                    index = 0;
                }
                else if (Target.position.x > Tf.position.x + 1 && Target.position.x < Tf.position.x + 2)
                {
                    index = 1;
                }
                else if (Target.position.x > Tf.position.x + 2 && Target.position.x < Tf.position.x + 3)
                {
                    index = 2;
                }
                else if (Target.position.x > Tf.position.x + 3 && Target.position.x < Tf.position.x + 4)
                {
                    index = 3;
                }
                else if (Target.position.x > Head.position.x + 4)
                {
                    index = 4;
                }
            }
            else
            {
                if (Target.position.x > Tf.position.x - 1 && Target.position.x < Tf.position.x + 1)
                {
                    index = 0;
                }
                else if (Target.position.x > Tf.position.x - 2 && Target.position.x < Tf.position.x - 1)
                {
                    index = 1;
                }
                else if (Target.position.x > Tf.position.x - 3 && Target.position.x < Tf.position.x - 2)
                {
                    index = 2;
                }
                else if (Target.position.x > Tf.position.x - 4 && Target.position.x < Tf.position.x - 3)
                {
                    index = 3;
                }
                else if (Target.position.x < Tf.position.x - 4)
                {
                    index = 4;
                }
            }
            #endregion

            Sr.sprite = Rotation[index];
            Sr.flipX = IsFlipped;

            if (Mult == 1)
            {
                Head.position = new Vector3(XOffset[index], Head.position.y, Head.position.z);
                ShootPoint.position = new Vector3(-XOffsetShoot[index], ShootPoint.position.y, ShootPoint.position.z);
            }
            else
            {
                Head.position = new Vector3(-XOffset[index], Head.position.y, Head.position.z);
                ShootPoint.position = new Vector3(XOffsetShoot[index], ShootPoint.position.y, ShootPoint.position.z);
            }

            for (int i = 0; i < Segments.Length; i++)
            {
                Segments[i].position = new Vector3(Tf.position.x + ShakeAmp * Mathf.Sin(T * ShakeFreq + 180 * (i % 2) + Segments[i].position.y), Segments[i].position.y, Segments[i].position.z);
            }

            if (T2 >= 1f && !Exploded && Preamble > 5)
            {
                Vector3 difference = Target.position - ShootPoint.position;
                float AngleOffset = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - (180f * .5f);
                BulletPatternsModule.ShootArc(135f, 5, "TriangleBullet", ShootPoint, AngleOffset);
                T2 = 0;
            }

            if (!HasReached)
            {
                Tf.position += new Vector3(0f, RiseSpeed, 0f) * Time.deltaTime;
            }

            if (Tf.position.y > HeightLevel)
            {
                HasReached = true;
                Tf.position = new Vector3(Tf.position.x, HeightLevel, Tf.position.z);
            }
        }
        else if (Exploded)
        {
            for (int i = 0; i < Segments.Length; i++)
            {
                Segments[i].position += (Vector3)MovementSpeed * Time.deltaTime;
                MovementSpeed.x *= -1;
                MovementSpeed.y -= 9.81f * Time.deltaTime;
            }
            MovementSpeed.x *= -1;
        }
    }

    void Explode()
    {
        EnemyScript.EnemyAmount--;
        StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
        AudioManager.instance.Play("MidExplosion");
        MovementSpeed = new Vector2(12f, 9.81f);
        Exploded = true;
    }
}
