using UnityEngine;
using System.Collections;
public class ScrapBossAI : StateMachineBehaviour
{
    public ScrapEvents SE;
    public float T;
    public float T2;
    public float health;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SE = GameObject.Find("ScrapBoss").GetComponent<ScrapEvents>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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

                if (T > 10)
                {
                }
                break;
        }
    }
}
