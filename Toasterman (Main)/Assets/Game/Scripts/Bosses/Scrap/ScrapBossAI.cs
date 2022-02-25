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
            case 1000:
                health = 2;
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
        }

        if (ScrapEvents.State == 1)
        {
            if (T > 2f)
            {
                SE.Shoot5Way();
                T = 0;
            }
        }
    }
}
