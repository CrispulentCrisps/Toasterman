using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAi : MonoBehaviour
{
    private Camera cam;
    private float MinShootTime = 1f;
    private float MaxShootTime = 5f;
    public float Full;
    public float Recharge;
    public float Charge = 0f;
    private float width;

    public bool Shooting = true;

    ObjectPools objectPooler;

    public Animator GunAnim;

    void Start()
    {

        Full = Random.Range(MinShootTime, MaxShootTime);
        objectPooler = ObjectPools.Instance;
        cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {

        if (Shooting == true)
        {

            Charge += Recharge * Time.deltaTime;

            if (Charge >= Full && transform.position.x >= -width && transform.position.x <= width)
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
