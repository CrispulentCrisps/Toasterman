﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStuff : MonoBehaviour, IPooledObject
{
    public Animator anim;

    public string SoundName;

    ObjectPools objectPooler;

    private float Time = 0f;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void Update()
    {

        Time += 0.1f;

        if (Time >= 2.5f)
        {

            gameObject.SetActive(false);

        }

    }

    public void OnObjectSpawn()
    {
        FindObjectOfType<AudioManager>().Play(SoundName);
        anim.SetTrigger("Boom");
        Time = 0;
    }
}
