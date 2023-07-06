using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapAnimEvents : MonoBehaviour
{
    public ParalaxStuff ps;

    public Transform TailTf;

    public Sprite[] sprite;

    public Vector3 TailMov;

    public float Health = 1500f;
    private float T;

    private bool ExplodeTail;
    private bool ExplodeTailGravity;

    ObjectPools objectPooler;

    public void Start()
    {
        ps = GameObject.Find("BG stuff").GetComponent<ParalaxStuff>();
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        T += Time.deltaTime;
        TailTf.Translate(TailMov * Time.deltaTime);
        if (ExplodeTail)
        {
            if (T >= 0.1f)
            {
                for (int i = 0; i < 10; i++)
                {
                    objectPooler.SpawnFromPool("ExplosionBig", new Vector3(TailTf.position.x + Random.Range(0f, -25f), TailTf.position.y, TailTf.position.z), Quaternion.identity);
                }
                T = 0f;
            }
        }
        if (ExplodeTailGravity)
        {
            TailMov.z -= 9.81f * 2 * Time.deltaTime;
        }
    }

    public void ChangeBG()
    {
        for (int i = 0; i < ps.Layer.Length; i++)
        {
            ps.Layer[i].GetComponent<SpriteRenderer>().sprite = sprite[i];
        }
    }

    public void SpawnLightning()
    {
        for (int i = 0; i < 2; i++)
        {
            objectPooler.SpawnFromPool("Lightning", new Vector3(Random.Range(-14f, 14f), 0f, 0f), Quaternion.identity);
            AudioManager.instance.ChangePitch("Strike", Random.Range(0.25f, 1.75f));
            AudioManager.instance.Play("Strike");
        }
    }

    public void Explode()
    {
        ExplodeTail = true;
    }
    public void ShootTailOff()
    {
        TailMov.y = 1f;
        TailMov.z += 9.81f;
        ExplodeTailGravity = true;
    }
    public void ChangeState(int StateNum)
    {
        gameObject.GetComponent<ScrapEvents>().State = StateNum;
    }
    
    public void StopLevelMusic()
    {
        StartCoroutine(AudioManager.instance.FadeAudio("ScrapyardTheme", 1f));
    }

    public void StartBossMusic()
    {
        AudioManager.instance.Play("ScrapBossTheme");
    }
}
