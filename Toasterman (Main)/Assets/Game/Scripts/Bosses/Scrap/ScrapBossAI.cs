using UnityEngine;
using System.Collections;
public class ScrapBossAI : StateMachineBehaviour
{
    public ScrapEvents SE;
    public Transform BodyTf;
    public Transform AnimTf;
    public Transform PlayerTf;
    public Transform SpawnPoint;
    public Transform EyeTf;
    Vector3 STrans;
    float Amp = 0;
    public float T;
    public float T2;
    public float health;
    public static bool StartAmp = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SE = GameObject.Find("ScrapBoss").GetComponent<ScrapEvents>();
        PlayerTf = GameObject.Find("Ship").GetComponent<Transform>();
        BodyTf = GameObject.Find("FollowPoint").GetComponent<Transform>();
        AnimTf = GameObject.Find("P3ANIM").GetComponent<Transform>();
        SpawnPoint = GameObject.Find("TentacleParent").GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("State = " + SE.State + ": Health = " + health);
        T += Time.deltaTime;
        T2 += Time.deltaTime;
		if (health <= 5500 && health > 5000)//Second phase
        {
            SE.RemoveTailColliders();
            SE.State = 2;
            animator.SetTrigger("P1");
		}
        else if (health <= 5000 && health > 4500)//Third phase
        {
            SE.State = 3;
            animator.SetTrigger("P2");
        }
        else if (health <= 4500 && health > 4000)//Fourth phase
        {
            SE.State = 4;
            animator.SetTrigger("P3");
        }
        else if (health <= 4000 && health > 3500)//Fourth phase
        {
            SE.State = 5;
            animator.SetTrigger("P4");
        }
        else if (health <= 3500 && health > 3000)//Fifth phase
        {
            SE.State = 5;
            animator.SetTrigger("P5");
        }

        switch (SE.State)
        {
            case 1:
                if (T > 2f)
                {
                    SE.Shoot5Way();
                    T = 0;
                }
                break;
            case 2:
                if (T2 > 3.75f)
                {
                    SE.ShootGun2();
                    T2 = 0;
                }
                if (T > 2.97f)
                {
                    SE.ShootGun();
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
                        Debug.Log("AMPLITUDE: " + Amp);

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
                if (T >= 1f)
                {
                    ObjectPools.Instance.SpawnFromPool("PurityLaser", EyeTf.position, Quaternion.identity);
                }
                break;
        }
    }
}