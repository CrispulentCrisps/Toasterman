using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRunToPlayer : StateMachineBehaviour
{
    public GameObject Self;

    public Transform tf;
    public Transform[] Destructibles;
    public Transform Target;
    public Transform LaserPoint;
    public Transform LaserPoint2;
    public Transform LaserPoint3;

    public Vector2[] DestructiblesMovement;
    public Vector2 Movement;

    public float Speed;
    public float TimeCount;
    public float TimeThresh;
    
    private float LaserTimer;
    private float DestroyTimer;

    public static float Health = 100f;
    public int State;

    private int RecursionInt;

    public static bool Destroyed = false;

    ObjectPools objectPooler;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//Start
    {
        Self = GameObject.Find("ROBOT");
        tf = Self.GetComponent<Transform>();
        Target = GameObject.Find("Ship").GetComponent<Transform>();
        LaserPoint = GameObject.Find("LaserShootPoint").GetComponent<Transform>();
        LaserPoint2 = GameObject.Find("LaserPoint2").GetComponent<Transform>();
        LaserPoint3 = GameObject.Find("LaserPoint3").GetComponent<Transform>();
        TimeThresh = Random.Range(1f,5f);
        State = 0;
        Destructibles = new Transform[] {GameObject.Find("LEGFRONT").GetComponent<Transform>(), 
            GameObject.Find("LEGBACC").GetComponent<Transform>(), 
            GameObject.Find("ARMFRONT").GetComponent<Transform>(), 
            GameObject.Find("ARMBACK").GetComponent<Transform>(), 
            GameObject.Find("BODY").GetComponent<Transform>() };
        objectPooler = ObjectPools.Instance;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//Update
    {
        TimeCount += Time.deltaTime;
        Movement.x = Speed;
        if (Health > 0f)
        {
            if (tf.position.y >= Target.position.y || tf.position.y >= 2f)
            {
                Movement.y = -2f;
            }
            else if (tf.position.y <= Target.position.y)
            {
                Movement.y = 2f;
            }

            if (State <= 0)
            {
                if (tf != null && tf.position.x <= Target.position.x)
                {
                    Speed += 5f * Time.deltaTime;
                }
                else if (tf != null && tf.position.x >= Target.position.x)
                {
                    Speed -= 5f * Time.deltaTime;
                }
            }
            else if (State != 0)
            {
                if (tf != null && tf.position.x <= -10f)
                {
                    Speed += 5f * Time.deltaTime;
                }
                else if (tf != null && tf.position.x >= -10f)
                {
                    Speed -= 5f * Time.deltaTime;
                }
            }
            if (Speed <= -7.5f)
            {
                Speed = -7.5f;
            }
            else if (Speed >= 7.5f)
            {
                Speed = 7.5f;
            }

            if (TimeCount >= TimeThresh)
            {
                State = Random.Range(0, 3);
                TimeCount = 0f;
                TimeThresh = Random.Range(5f, 7f);
            }

            switch (State)
            {
                case 0:
                    animator.SetTrigger("Run");
                    break;
                case 1:
                    animator.SetTrigger("Laser");
                    break;
                case 2:
                    animator.SetTrigger("Rand");
                    break;
            }

            if (State == 1)
            {
                LaserTimer += Time.deltaTime;
                if (LaserTimer >= 0.5f)
                {
                    RecursionInt++;
                    BulletPatternsModule.ShootArc(360f, 8, "EnemyBulletUp", LaserPoint, -90f);
                    LaserTimer = 0f;
                }
            }
            else if (State == 2)
            {
                LaserTimer += Time.deltaTime + Random.Range(0.05f, 0.25f);
                if (LaserTimer >= 4f)
                {
                    RecursionInt++;
                    BulletPatternsModule.ShootArc(180f, 4, "EnemyBulletUp", LaserPoint2, 180f);
                    BulletPatternsModule.ShootArc(180f, 4, "EnemyBulletUp", LaserPoint3, 180f);
                    LaserTimer = 0f;
                }
            }
            DestroyTimer = 0f;
        }
        else
        {
            DestroyTimer += Time.deltaTime;
            TimeCount += Time.deltaTime;
            //Move to center
            if (tf.position.y >= 0f)
            {
                Movement.y = -2f;
            }
            else if (tf.position.y <= 0f)
            {
                Movement.y = 2f;
            }

            if (tf.position.x >= 0f)
            {
                Movement.x = -2f;
            }
            else if (tf.position.x <= 0f)
            {
                Movement.x = 2f;
            }
            //Explosions and smoke
            if (TimeCount >= 0.1f && Destroyed)
            {
                for (int i = 0; i < Destructibles.Length; i++)
                {
                    AudioManager.instance.ChangePitch("Explosion", UnityEngine.Random.Range(.1f, .75f));
                    AudioManager.instance.Play("Explosion");
                    objectPooler.SpawnFromPool("BigExplosion", new Vector3(Destructibles[i].position.x + Random.Range(-2f, 2f), Destructibles[i].position.y + Random.Range(0f, 4f), 0f), Quaternion.identity);
                }
                TimeCount = 0f;
            }
            
            if (TimeCount >= 0.1f)
            {
                for (int i = 0; i < Destructibles.Length; i++)
                {
                    objectPooler.SpawnFromPool("Smoke", new Vector3(Destructibles[i].position.x + Random.Range(-2f, 2f), Destructibles[i].position.y + Random.Range(0f, 4f), 0f), Quaternion.identity);
                }
                TimeCount = 0f;
            }

            if (DestroyTimer >= 5f)
            {
                for (int i = 0; i < Destructibles.Length; i++)
                {
                    DestructiblesMovement[i] = new Vector2(Random.Range(5f, 15f), Random.Range(5f, 15f));
                }
                Destroyed = true;
                DestroyTimer = -9999f;
            }
            if (Destroyed)
            {
                for (int i = 0; i < Destructibles.Length; i++)
                {
                    DestructiblesMovement[i] -= new Vector2(5f,9.81f) * Time.deltaTime;
                    Destructibles[i].Translate(DestructiblesMovement[i] * Time.deltaTime);
                    if (Destructibles[i].position.y <= -7f)
                    {
                        DestructiblesMovement[i] = new Vector2(DestructiblesMovement[i].x, DestructiblesMovement[i].y * -0.9f);
                    }
                    if (Destructibles[i].position.x <= -75f)
                    {
                        Self.SetActive(false);
                    }
                }
            }
        }

        tf.Translate(Movement * Time.deltaTime);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
