using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMiniAI : StateMachineBehaviour
{
    public Transform tf; //0 = Main transform, 1-4 = shroom caps

    public float Speed;
    public static float Health = 400f;
    //Sine wave stuff
    public float Amp;
    public float Freq;
    public float STMult;
    private float ST;
    private float T;//Time for attacking
    private float XPos = -20f;
    public static bool Move = true;
    private bool Left;
    public static bool FindTrans = false;

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (FindTrans)
        {
            tf = animator.GetComponent<Transform>();
            tf.position = new Vector3(-20f, 0f, 0f);
            AudioManager.instance.Play("ShroomEnter");
            FindTrans = false;
        }
        if (Move && Health > 0f)
        {
            ST += Time.deltaTime * STMult;//Sine time
        }

        T += Time.deltaTime * Random.Range(0.01f,1f);//Time for attacks
        
        if (Health > 0f)//Checking if alive
        {
            tf.position = new Vector3(XPos, Amp * Mathf.Sin(ST * Freq) + 1f, 0f);
            if (Left)
            {
                XPos += Speed * Time.deltaTime;
            }
            else
            {
                XPos -= Speed * Time.deltaTime;
            }

            if (tf.position.x <= -12f)
            {
                Left = true;
            }
            else if (tf.position.x >= 11f) //Has to be 11 to adjust for the centers offset
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
            tf.position -= Vector3.MoveTowards(tf.position, new Vector3(3f, 3f, 0), Time.deltaTime) * Time.deltaTime; //Moves miniboss to desired coords
            Move = false;
            if (tf.position.x >= -0.25f && tf.position.x <= 1.25f && tf.position.y >= -0.25f && tf.position.y <= 1.25f)
            {
                animator.SetTrigger("Death");
            }
        }
    }
}
