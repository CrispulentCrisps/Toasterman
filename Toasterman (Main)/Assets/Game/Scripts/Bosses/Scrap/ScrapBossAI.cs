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
        Debug.Log("State = " + ScrapEvents.State + ": Health = " + health);
        T += Time.deltaTime;
        switch (health)
        {
            case 5500:
                ScrapEvents.State = 2;
                animator.SetTrigger("P1");
                break;
            case 5000:
                ScrapEvents.State = 3;
                animator.SetTrigger("P2");
                break;
        }

        switch (ScrapEvents.State)
        {
            case 1:
                if (T > 2f)
                {
                    SE.Shoot5Way();
                    T = 0;
                }
                break;
            case 2:
                if (T > 2f)
                {
                    T = 0;
                }
                break;
        }
    }
}
