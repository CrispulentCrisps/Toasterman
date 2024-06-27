using UnityEngine;
using System.Collections;
public class ScrapBossAI : StateMachineBehaviour
{
    public ScrapEvents SE;
    public Transform AnimTf;
    public Transform PlayerTf;
    public Transform SpawnPoint;
    public Transform EyeTf;
    Vector3 STrans;
    float Amp = 0;
    public float T;
    public float T2;
    public static float health = 2500f;
    public static bool StartAmp = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("IsDead"))
        {
            SE = GameObject.Find("ScrapBoss(Clone)").GetComponent<ScrapEvents>();
            PlayerTf = GameObject.Find("Ship").GetComponent<Transform>();
            AnimTf = GameObject.Find("P3ANIM").GetComponent<Transform>();
            SpawnPoint = GameObject.Find("TentacleParent").GetComponent<Transform>();
            EyeTf = GameObject.Find("Pupil").GetComponent<Transform>();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("State = " + SE.State + ": Health = " + health);
        T += Time.deltaTime;
        T2 += Time.deltaTime;
		if (health <= 2000 && health > 1500)//Second phase
        {
            SE.RemoveTailColliders();
            SE.State = 2;
            animator.SetTrigger("P1");
		}
        else if (health <= 1500 && health > 1000)//Third phase
        {
            SE.State = 3;
            animator.SetTrigger("P2");
        }
        else if (health <= 1000 && health > 500)//Fourth phase
        {
            SE.State = 4;
            animator.SetTrigger("P3");
        }
        else if (health <= 500 && health > 0)//Fourth phase
        {
            SE.State = 5;
            animator.SetTrigger("P4");
        }
        else if (health <= 0)//Fifth phase
        {
            SE.State = 6;
            animator.SetTrigger("P5");
        }

        switch (SE.State)
        {
            case 1:
                if (T > 2f)
                {
                    SE.Shoot5Way();
                    AudioManager.instance.Play("EnemyShoot3");
                    T = 0;
                }
                break;
            case 2:
                if (T2 > 3.75f)
                {
                    SE.ShootGun2();
                    AudioManager.instance.Play("EnemyShoot2");
                    T2 = 0;
                }
                if (T > 2.97f)
                {
                    SE.ShootGun();
                    AudioManager.instance.Play("EnemyShoot2");
                    T = 0;
                }
                break;
            case 3:
                if (!SE.IsAttacking)
                {
                    if (PlayerTf.position.y > AnimTf.position.y + 5f)
                    {
                        AnimTf.position += new Vector3(0f, 5f, 0f) * Time.deltaTime;
                    }
                    else if (PlayerTf.position.y < AnimTf.position.y + 5f)
                    {
                        AnimTf.position -= new Vector3(0f, 5f, 0f) * Time.deltaTime;
                    }
                }
                break;
            case 4:
                if (StartAmp)
                {
                    if (Amp < 5f)
                    {
                        Amp += 2f * Time.deltaTime;

                    }
                    else if (Amp > 5f)
                    {
                        Amp = 5f;
                    }
                }
                else
                {
                    Amp = 0;
                }
                AnimTf.position = Vector3.MoveTowards(AnimTf.position, new Vector3(SE.STrans.x, SE.STrans.y, 0f), 1f * Time.deltaTime);
                if (T > 4f && SE.StartSpawning == true)
                {
                    ObjectPools.Instance.SpawnFromPool("PurityShooter", SpawnPoint.position, Quaternion.identity);
                    T = 0;
                }
                else if (T2 > 1.5f && SE.StartSpawning == true)
                {
                    ObjectPools.Instance.SpawnFromPool("HomingPurity", SpawnPoint.position, Quaternion.identity);
                    T2 = 0;
                }
                break;
            case 5:
                if (PlayerTf.position.y > AnimTf.position.y + 5f)
                {
                    AnimTf.position += new Vector3(0f, 1f, 0f) * Time.deltaTime;
                }
                else if (PlayerTf.position.y < AnimTf.position.y + 5f)
                {
                    AnimTf.position -= new Vector3(0f, 1f, 0f) * Time.deltaTime;
                }
                if (T >= 1f && SE.firing)
                {
                    T = 0;
                    AudioManager.instance.Play("Electricity");
                    AudioManager.instance.ChangePitch("Electricity", 2f);
                    ObjectPools.Instance.SpawnFromPool("PurityLaser", new Vector3(EyeTf.position.x + 2f, EyeTf.position.y, EyeTf.position.z), Quaternion.identity);
                }
                break;
            case 6:
                T = -99f;
                AnimTf.position = Vector3.MoveTowards(AnimTf.position, new Vector3(12f,-5f,0f), 4f * Time.deltaTime);
                animator.SetBool("IsDead", true);
                break;
        }
    }
}