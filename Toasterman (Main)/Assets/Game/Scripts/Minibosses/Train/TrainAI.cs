﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAI : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    public ParalaxStuff paralaxStuff;

    public Transform RoccTf;

    public float LifeTime;
    private float Life;

    public GunAi gunai;

    public RoccAI roccai;

    public bool Ended = false;

    public bool Started = false;

    void Start()
    {

        objectPooler = ObjectPools.Instance;


    }

    public void OnObjectSpawn()
    {

        Life = LifeTime;

        paralaxStuff = GameObject.FindGameObjectWithTag("BackGroundStuff").GetComponent<ParalaxStuff>();

        paralaxStuff.paraspeedGoal = 50f;

    }

    void Update()
    {


        Life -= 1f * Time.deltaTime;

        if (Life <= 0f && roccai.Ending == false)
        {

            RoccTf.position = new Vector3(16f,-2,0);

            gunai.Shooting = false;

            roccai.Ending = true;
        }

        if (Ended == true)
        {

            gameObject.SetActive(false);

        }

    }
}
