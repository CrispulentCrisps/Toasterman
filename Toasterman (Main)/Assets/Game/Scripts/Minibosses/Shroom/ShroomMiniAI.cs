using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMiniAI : StateMachineBehaviour
{
    public Transform[] Movables; //0 = Main transform, 1-4 = shroom caps

    public float Speed;
    public static float Health = 250f;
    //Sine wave stuff
    public float Amp;
    public float Freq;
    public float STMult;
    private float ST;
    private float T;//Time for attacking
    private float XPos = -20f;

    public static bool Move = true;
    private bool Left;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Movables[0] = GameObject.Find("ShroomMinibossMaster").GetComponent<Transform>();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Move && Health > 0f)
        {
            ST += Time.deltaTime * STMult;//Sine time
        }

        T += Time.deltaTime * Random.Range(0.01f,1f);//Time for attacks
        
        if (Health > 0f)
        {
            Movables[0].position = new Vector3(XPos, Amp * Mathf.Sin(ST * Freq) + 1f, 0f);
            if (Left)
            {
                XPos += Speed * Time.deltaTime;
            }
            else
            {
                XPos -= Speed * Time.deltaTime;
            }

            if (Movables[0].position.x <= -12f)
            {
                Left = true;
            }
            else if (Movables[0].position.x >= 11f) //Has to be 11 to adjust for the centers offset
            {
                Left = false;
            }
            if (T > 3f)
            {
                int State = Random.Range(0, 2);
                if (State == 0)
                {
                    animator.SetTrigger("Attack");
                }
                else if (State == 1)
                {
                    animator.SetTrigger("Center");
                }
                T = 0f;
            }
        }
        else //Death stuff
        {
            Movables[0].position -= Vector3.MoveTowards(Movables[0].position, new Vector3(-0f, -0f, 0), Time.deltaTime) * Time.deltaTime; //Moves miniboss to desired coords
            if (Movables[0].position.x >= -0.25f && Movables[0].position.x <= 0.25f && Movables[0].position.y >= -0.25f && Movables[0].position.y <= 0.25f)
            {
                animator.SetTrigger("Death");
            }
        }
    }
}
