using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Transform tf;

    ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void Shoot()
    {
        objectPooler.SpawnFromPool("BombWavyDown", new Vector3(tf.position.x-3, tf.position.y, tf.position.z), Quaternion.identity);
    }
}
