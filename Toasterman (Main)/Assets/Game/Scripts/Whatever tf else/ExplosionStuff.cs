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
        FindObjectOfType<AudioManager>().Play(SoundName);
        anim.SetTrigger("Boom");
        time = 0;
    }
}
