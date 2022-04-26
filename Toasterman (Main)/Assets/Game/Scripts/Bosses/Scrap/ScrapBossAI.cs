using UnityEngine;

public class ScrapBossAI : StateMachineBehaviour
{
    public ScrapEvents SE;
    public float T;
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
		if (health <= 5500 && health > 5000)//Second phase
        {
            SE.RemoveTailColliders();
            SE.State = 2;
            animator.SetTrigger("P1");
		}
        else if (health <= 5500 && health > 5000)//Third phase
        {
            SE.State = 3;
            animator.SetTrigger("P2");
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
                if (T > 4f)
                {
                    SE.ShootGun();
                    T = 0;
                }
                break;
        }
    }
}
