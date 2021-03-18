using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapAnimEvents : MonoBehaviour
{
    public ParalaxStuff ps;

    public Sprite[] sprite;

    public static float Health = 1500f;

    ObjectPools objectPooler;

    public void Start()
    {
        ps = GameObject.Find("BG stuff").GetComponent<ParalaxStuff>();
        objectPooler = ObjectPools.Instance;
    }

    public void ChangeBG()
    {
        for (int i = 0; i < ps.Layer.Length; i++)
        {
            ps.Layer[i].GetComponent<SpriteRenderer>().sprite = sprite[i];
        }
        ps.ParaDampen = 3f;
    }

    public void SpawnLightning()
    {
        for (int i = 0; i < 3; i++)
        {
            objectPooler.SpawnFromPool("Lightning", new Vector3(Random.Range(-14f, 14f), 0f, 0f), Quaternion.identity);
            AudioManager.instance.ChangePitch("Strike", Random.Range(0.5f, 1.5f));
            AudioManager.instance.Play("Strike");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Health:" + Health);
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
    }

    public void ChangeState(int StateNum)
    {
        ScrapBossAI.State = StateNum;
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
