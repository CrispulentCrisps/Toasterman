﻿using UnityEngine;

public class Shooting : MonoBehaviour
{ 
    public Transform tf;

    ObjectPools objectPooler;

    private float FireRate = 0f;
    public float ReloadTime = 10f;
    public float Increment;

    private bool Auto = false;

    public int BulletType;
    public int BulletLevel = 1;

    private float BulletSpreadMult;

    public Quaternion BulletRot;

    public GameObject[] Projectiles;

    private string[] ProjectileNames;


    private void Start()
    {

        objectPooler = ObjectPools.Instance;

        ProjectileNames = new string[] { "Bullet", "Acid" };

    }

    // Update is called once per frame
    void Update()
    {

       if (BulletType == 1)
        {

            Increment *= 1.25f;

        }

        FireRate += Increment * Time.deltaTime;

 

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.M))// shooting input
        {

            Auto = true;

        }
        else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.M))
        {

            Auto = false;

        }
        else if (Input.GetKeyDown(KeyCode.X))// shooting input
        {

            Auto = true;

        }


        if (Auto == true && FireRate >= ReloadTime)
        {

            Shoot();

        }

        
    }


    public void Shoot()
    {
        switch (BulletType)
        {
            case 0:
                for (int i = 0; i < BulletLevel; i++)// spread shot
                {
                    //Spread
                    BulletSpreadMult = BulletLevel * 1.5f;
                    BulletRot = Quaternion.Euler(0, 0, ((BulletLevel + ((i - BulletLevel / 2) * BulletSpreadMult)) % 360));
                    objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, BulletRot);

                }
                break;
            case 1:
                    objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
        }
        FireRate = 0;// reset firerate

    }

}