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
    //Arcs
    public static void ShootArc(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset)//All arcs are in angles, not radians
    {
        float angle = Offset;//Offset is to the left
        float AngleStep = ArcSize / BulletAmount;//Gets the step size for arc
        for (int i = 0; i < BulletAmount; i++)
        {
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));//Shoots the bullet
        }
    }
    public static IEnumerator ShootArcEnum(float ArcSize, int BulletAmount, string BulletName, Transform tf, float Offset, float WaitPeriod)//All arcs are in angles, not radians
    {
        float angle = Offset;//Offset is to the left
        float AngleStep = ArcSize / BulletAmount;
        for (int i = 0; i < BulletAmount; i++)
        {
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(WaitPeriod);
        }
    }
    //Lines
    public static void ShootLine(float BaseSpeed, float MinVel, float MaxVel, int BulletAmount, string BulletName, Transform tf, float angle)//All arcs are in angles, not radians
    {
        float Difference = (MaxVel - MinVel + 1) / BulletAmount;
        for (int i = 0; i < BulletAmount; i++)
        {
            objectPooler.SpawnBulletFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle), new Vector2(BaseSpeed + Difference * i, 0f));//Shoots the bullet
        }
    }
    public static IEnumerator ShootLineEnum(float BaseSpeed, float MinVel, float MaxVel, int BulletAmount, string BulletName, Transform tf, float angle, float WaitPeriod)//All arcs are in angles, not radians
    {
        float Difference = (MaxVel - MinVel + 1) / BulletAmount;
        for (int i = 0; i < BulletAmount; i++)
        {
            objectPooler.SpawnBulletFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle), new Vector2(BaseSpeed + Difference * i, 0f));//Shoots the bullet
            yield return new WaitForSeconds(WaitPeriod);
        }
    }
    //Lines+arcs
    public static void ShootArcLine(float BaseSpeed, float MinVel, float MaxVel, int BulletAmount, string BulletName, Transform tf, float Offset, float ArcSize, int ArcRepeat)//All arcs are in angles, not radians
    {
        float Difference = (MaxVel - MinVel + 1) / BulletAmount;
        float angle = Offset - (ArcSize / ArcRepeat) * 0.5f;//Offset is to the left
        float AngleStep = (ArcSize / ArcRepeat);
        for (int j = 0; j < ArcRepeat; j++)
        {
            angle += AngleStep;
            for (int i = 0; i < BulletAmount; i++)
            {
                objectPooler.SpawnBulletFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle), new Vector2(BaseSpeed + Difference * i, 0f));//Shoots the bullet
            }
        }
    }
}
