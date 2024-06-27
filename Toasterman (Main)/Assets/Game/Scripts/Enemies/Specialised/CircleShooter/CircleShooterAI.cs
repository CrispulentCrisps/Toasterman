using System.Collections;
using UnityEngine;

public class CircleShooterAI : EnemyShootScript
{
    [SerializeField] protected float Timer;
    [SerializeField] protected float MiniInterval;
    [SerializeField] protected float Interval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 3)
        {
            StartCoroutine(FireBullets());
            Timer = -3;
        }
    }

    IEnumerator FireBullets()
    {
        Vector3 difference = Target.position - tf.position;
        AngleOffset = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 22.5f;
        GapOffset = AngleOffset;
        for (int i = 0; i < 5; i++)
        {
            BulletPatternsModule.ShootArcGap(RegularAngle, GapSize, GapOffset, RegularAmount, BulletName, tf, AngleOffset);
            yield return new WaitForSeconds(MiniInterval);
        }
        yield return new WaitForSeconds(Interval);
        for (int i = 0; i < 5; i++)
        {
            BulletPatternsModule.ShootArc(GapSize*0.5f, RegularAmount, BulletName, tf, GapOffset);
            yield return new WaitForSeconds(MiniInterval);
        }
    }
}
