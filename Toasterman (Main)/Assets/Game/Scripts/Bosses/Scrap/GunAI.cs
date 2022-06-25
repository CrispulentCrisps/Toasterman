using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAI : MonoBehaviour
{
    public Transform CannonPoint;
    private void Start()
    {
        CannonPoint = gameObject.transform;
    }
    public void FireBullet()
    {
        Transform tf = CannonPoint;
        tf.position = new Vector3(CannonPoint.position.x - 3, CannonPoint.position.y, CannonPoint.position.z);
        BulletPatternsModule.ShootArc(0f, 1, "GunBomb", tf, 0f);
    }
}
