using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternsModule : MonoBehaviour
{
    static ObjectPools objectPooler;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public static void ShootArc(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset)//All arcs are in angles, not radians
    {
        float angle = 0;
        angle = Offset;//Offset is to the left
        for (int i = 0; i < BulletAmount; i++)
        {
            float AngleStep = ArcSize / BulletAmount;//Gets the step size for arc
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));//Shoots the bullet
        }
    }

    public static IEnumerator ShootArcEnum(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset, float LengthOfTime, float WaitPeriod)//All arcs are in angles, not radians
    {
        float angle = 0;
        angle = Offset;//Offset is to the left
        for (int i = 0; i < BulletAmount; i++)
        {
            float AngleStep = ArcSize / BulletAmount;
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(WaitPeriod);
        }
    }
}
