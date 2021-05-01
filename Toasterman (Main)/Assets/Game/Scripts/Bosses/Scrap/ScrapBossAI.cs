using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBossAI : StateMachineBehaviour
{
    public ParalaxStuff ps;

    public GameObject Self;
    public Transform tf;
    public Transform Target;
    public Transform[] ShotPoints;
    public Transform[] HeadParts;

    public Animator[] GunAnim;

    public string[] BulletNames;

    public static float Health = 1500f;

    private float T;

    public static int State = -1;

    ObjectPools objectPooler;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Self = GameObject.Find("ScrapBoss");
        tf = Self.GetComponent<Transform>();
        Target = GameObject.Find("Ship").GetComponent<Transform>();
        ps = GameObject.Find("BG stuff").GetComponent<ParalaxStuff>();
        ShotPoints[0] = GameObject.Find("SHOTPOINTTAIL").GetComponent<Transform>();
        ShotPoints[1] = GameObject.Find("TAILARMATURE").GetComponent<Transform>();
        for (int i = 0; i < 2; i++)
        {
            GunAnim[i] = GameObject.Find("GUN" + (i+1)).GetComponent<Animator>();
            ShotPoints[i + 2] = GameObject.Find("GUN" + (i + 1)).GetComponent<Transform>();
        }
        ps.paraspeedGoal = 125f;
        objectPooler = ObjectPools.Instance;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        T += Time.deltaTime;

        if (Health <= 1250f && Health > 1000f)
        {
            animator.SetTrigger("TailDestroyed");
        }

        Debug.Log(Health);

        switch (State)
        {
            default:
                break;
            case 0:
                if (T >= 0.69f)
                {
                    BulletPatternsModule.ShootArc(135f, 6, BulletNames[0], ShotPoints[0], 0f);
                    T = 0f;
                }
                break;
            case 1:
                if (T >= 1.95f)
                {
                    for (int i = 0; i < GunAnim.Length; i++)
                    {
                        GunAnim[i].SetTrigger("Fire");
                    }
                    T = 0;
                }
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
