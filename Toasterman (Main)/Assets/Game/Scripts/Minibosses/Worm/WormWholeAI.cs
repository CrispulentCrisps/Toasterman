using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormWholeAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    public Transform Target;

    public Rigidbody2D rb;

    public GameObject Ship;
    public GameObject WaveMaker;
    public EnemyScript enemyscript;

    ObjectPools objectPooler;

    public float JumpForce;
    private float Timer;
    private int TimerFull;
    public float Health;
    private float TargetPosX;

    public bool Alive;
    private bool RockShot;

    public void OnObjectSpawn()
    {

        WaveMaker = GameObject.Find("EnemyWaveMaker");//gets the game object

        Ship = GameObject.Find("Ship");//gets the game object
        Target = Ship.GetComponent<Transform>();// gets the Transform 

        TimerFull = Random.Range(1, 4);
    }

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void Update()
    {
        if (tf.position.y <= -10f)
        {

            Timer += Time.deltaTime;

        }
        
        if (Timer >= TimerFull && tf.position.y <= -10f && Alive == true)
        {

            rb.velocity = Vector3.zero;

            tf.position = new Vector3(TargetPosX, -9f, 0f);

            rb.AddForce(tf.up * JumpForce);

            Timer = 0f;

            RockShot = false;

        }
        else if (Timer >= TimerFull - 1f && tf.position.y <= -10f && Alive == true && RockShot == false)
        {

            TargetPosX = Target.position.x + Random.Range(-1f, 1f);

            tf.position = new Vector3(TargetPosX, -9f, 0f);

            objectPooler.SpawnFromPool("Rocks", new Vector3(tf.position.x,-7f,tf.position.z),Quaternion.EulerAngles(-90f,0f,0f));
            AudioManager.instance.ChangePitch("WormEnter", Random.Range(1f, 0.5f));
            AudioManager.instance.Play("WormEnter");
            RockShot = true;

        }

    }


}
