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
        float AngleStep = ArcSize / BulletAmount;//Gets the step size for arc
        for (int i = 0; i < BulletAmount; i++)
        {
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));//Shoots the bullet
        }
    }
    public static IEnumerator ShootArcEnum(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset, float LengthOfTime, float WaitPeriod)//All arcs are in angles, not radians
    {
        float angle = 0;
        angle = Offset;//Offset is to the left
        float AngleStep = ArcSize / BulletAmount;
        for (int i = 0; i < BulletAmount; i++)
        {
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(WaitPeriod);
        }
    }

    //Not quite done yet
    public static void ShootLine(float BaseSpeed, float MinVel, float MaxVel, int BulletAmount, string BulletName, Transform tf, float angle)//All arcs are in angles, not radians
    {
        float Difference = MaxVel - MinVel / BulletAmount;
        for (int i = 0; i < BulletAmount; i++)
        {
            objectPooler.SpawnBulletFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle), new Vector2(BaseSpeed + Difference * i, 0f));//Shoots the bullet
        }
    }
}
