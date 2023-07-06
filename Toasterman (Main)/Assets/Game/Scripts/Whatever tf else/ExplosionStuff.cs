using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStuff : MonoBehaviour, IPooledObject
{
    public Animator anim;

    public string SoundName;

    ObjectPools objectPooler;

    private float time = 0f;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= .5f)
        {
            gameObject.SetActive(false);
        }

    }

    public void OnObjectSpawn()
    {
        if (SoundName != "")
        {
            AudioManager.instance.Play(SoundName);
            AudioManager.instance.ChangePitch(SoundName, Random.Range(.75f, 1.25f));
        }
        anim.SetTrigger("Boom");
        time = 0;
    }
}
