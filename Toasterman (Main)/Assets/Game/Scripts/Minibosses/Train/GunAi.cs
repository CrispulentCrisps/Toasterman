using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAi : MonoBehaviour
{

    private float MinShootTime = 1f;
    private float MaxShootTime = 5f;
    public float Full;
    public float Recharge = 0.5f;
    public float Charge = 40f;
    public bool Shooting = true;

    ObjectPools objectPooler;

    public Animator GunAnim;

    void Start()
    {

        Full = Random.Range(MinShootTime, MaxShootTime);


        objectPooler = ObjectPools.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        if (Shooting == true)
        {

            Charge += Recharge * Time.deltaTime;

            if (transform.position.x <= -12f || transform.position.x >= 12f)
            {

                Charge = 99f;

            }


            if (Charge >= Full)
            {

                GunAnim.Play("BarrelShoot");

                Full = Random.Range(MinShootTime, MaxShootTime);

                Charge = 0f;

            }

        }

    }

    public void ShootLaser()
    {

        objectPooler.SpawnFromPool("EnemyBulletUp", new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

    }

}
