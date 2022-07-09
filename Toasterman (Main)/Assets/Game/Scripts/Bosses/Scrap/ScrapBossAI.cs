using UnityEngine;
using System.Collections;
public class ScrapBossAI : StateMachineBehaviour
{
    public ScrapEvents SE;
    public Transform BodyTf;
    public Transform AnimTf;
    public Transform PlayerTf;
    public float T;
    public float T2;
    public float health;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SE = GameObject.Find("ScrapBoss").GetComponent<ScrapEvents>();
        PlayerTf = GameObject.Find("Ship").GetComponent<Transform>();
        BodyTf = GameObject.Find("FollowPoint").GetComponent<Transform>();
        AnimTf = GameObject.Find("P3ANIM").GetComponent<Transform>();
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
                        Debug.Log("IS MOVING UP");
                        AnimTf.position += new Vector3(0f, 5f, 0f) * Time.deltaTime;
                    }
                    else if (PlayerTf.position.y < AnimTf.position.y + 5f)
                    {
                        Debug.Log("IS MOVING DOWN");
                        AnimTf.position -= new Vector3(0f, 5f, 0f) * Time.deltaTime;
                    }
                }
                break;
            case 4:

                break;
        }
    }
}
