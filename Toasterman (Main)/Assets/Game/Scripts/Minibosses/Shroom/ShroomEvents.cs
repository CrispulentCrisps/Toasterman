using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomEvents : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public EnemyScript es;

    public Transform[] TF;

    public SpriteRenderer[] sr;

    public Vector2[] Speed;

    public bool CapsShot = false;
    private float T;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        es = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();
        es.start = false;
    }

    public void Update()
    {
        if (CapsShot)
        {
            for (int i = 0; i < 4; i++)
            {
                TF[i + 5].Translate(Speed[i] * Time.deltaTime);
                Speed[i].y -= 9.81f * Time.deltaTime;
                Speed[i].x *= 0.99f;
                if (T >= 0.1f)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        objectPooler.SpawnFromPool("SporeExplosion", new Vector3(TF[j + 5].position.x + Random.Range(0.15f,-0.15f), TF[j + 5].position.y + Random.Range(0.15f, -0.15f), 0f), Quaternion.identity);
                    }
                    T = 0;
                }
            }
        }
        T += Time.deltaTime;
    }

    public void ShootCircle(int pos)
    {
        BulletPatternsModule.ShootArc(360f, 6, "PurityBullet", TF[pos], 0f);
        AudioManager.instance.ChangePitch("Shoot1", Random.Range(0.75f, 1.25f));
        AudioManager.instance.Play("Shoot1");
    }

    public void ShootCenter(float Offset)
    {
        StartCoroutine(BulletPatternsModule.ShootArcEnum(90f, 8, "PurityBullet", TF[0], Offset, 0.25f * 0.125f));
        StartCoroutine(BulletPatternsModule.ShootArcEnum(-90f, 8, "PurityBullet", TF[0], -Offset, 0.25f * 0.125f));
        AudioManager.instance.ChangePitch("Shoot1", Random.Range(0.75f, 1.25f));
        AudioManager.instance.Play("Shoot1");
    }

    public void CHangeMove()
    {
        ShroomMiniAI.Move = !ShroomMiniAI.Move;
    }

    public void ShootCaps()
    {
        Speed[0] = new Vector2(-15f,7.5f);
        Speed[1] = new Vector2(15f,7.5f);
        Speed[2] = new Vector2(-7.5f,5f);
        Speed[3] = new Vector2(7.5f,5f);
        CapsShot = true;
        es.start = true;
        AudioManager.instance.Play("ShootCaps");
    }

    public void ChangeLayer(string LayerName)
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].sortingLayerName = LayerName;
        }
    }
}
