using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrutleBossAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    public Transform Target;
    public Transform MissleShotSpot;
    public Transform TailShotSpot;
    public Transform BoomPoint;

    public Rigidbody rb;

    public GameObject missleHoming;

    ObjectPools objectPooler;

    public CameraShake camerashake;

    public ParalaxStuff paralaxStuff;

    public Animator anim;
    public Animator CoreAnim;
    public Animator BGAnim;

    public float SinAmp;
    public float SineFreq;

    public float Health;

    public float YSpeed;
    public float YVel;

    public float XSpeed;
    public float XVel;

    public float RegularAngle;
    public int RegularAmount;

    private int State = 99;
    
    private Quaternion BulletRot;

    public bool Intro = true;
    public bool Killed = false;
    private bool started = false;

    private float I; // regular shot angle

    public Vector3 Speed = new Vector3(25f,0f,0f);

    private float Timer;
    private float Timer2;



    void Start()
    {

        objectPooler = ObjectPools.Instance;

        tf.position = new Vector3(25,0,-1);
        Timer = 0;

        Target = GameObject.FindGameObjectWithTag("Player").transform;

        camerashake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

        CoreAnim = GameObject.FindGameObjectWithTag("Core").GetComponent<Animator>();

    }
    public void OnObjectSpawn()
    {

        tf.position = new Vector3(20, 0, -1);
        YSpeed = 0f;
        Timer = 0;
        Timer2 = 0;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        paralaxStuff = GameObject.FindGameObjectWithTag("BackGroundStuff").GetComponent<ParalaxStuff>();
        paralaxStuff.paraspeedGoal = 75f;
        BGAnim = GameObject.FindGameObjectWithTag("UninspiredBackground").GetComponent<Animator>();
        BGAnim.SetTrigger("AnimB");
        Intro = false;
        StartCoroutine(StartStuff());
    }
    public void FixedUpdate()
    {
        
        rb.MovePosition(rb.position - Speed * Time.deltaTime);

    }

    void Update()
    {

        Timer += Time.deltaTime;

        Speed.y = YSpeed;
        
        if (Health <= 0f && State != -1)
        {
            Timer = -1;

            if (tf.position.y > Target.position.y)
            {

                YSpeed = 5;

            }
            else if (tf.position.y < Target.position.y)
            {

                YSpeed = -5;

            }

            if (tf.position.y > -1f || tf.position.y > 1f)
            {
                anim.SetTrigger("Death");
            }

        }else if (Health <= 0f && State == -1)
        {

            XSpeed = XVel;
            XVel -= 1f;
            Speed.x -= XSpeed * Time.deltaTime;
            YSpeed = 0;

            if (tf.position.x <= -15 && Killed == false)
            {

                DieShake();
                Killed = true;
            }
        }

        if (Timer >= 2)
        {
            if (Health > 0f)
            {

                State = Random.Range(0, 4);

            }

            switch (State) //Attacks
            {
                case 1: anim.SetTrigger("Missle");
                    break;
                case 2:
                    anim.SetTrigger("Tail");
                    break;
                case 3:
                    anim.SetTrigger("BigHurt");
                    break;
                default:
                    anim.SetTrigger("Idle");
                    break;
            }
                Timer = 0;
        }

        if (Health <= 250f && Health > 0)
        {

            paralaxStuff.paraspeedGoal = 100f;
            Timer2 += Time.deltaTime;
            if (Timer2 >= 1.5f)
            {
                Booming();
                Timer2 = 0;
            }
        }

        //intro stuff
        if (tf.position.x > 7 && Intro == true)
        {

            XSpeed = 7.5f;
            Speed.x = XSpeed;

        }
        else if(tf.position.x <= 7 && Intro == true)
        {
            Speed.x = 0;
            Intro = false;
            started = true;
        }

        //Regular AI
        if (Intro == false && Health > 0f && started == true)
        {
            Speed.x = 0;
            YVel += Time.deltaTime;
            if (SinAmp <= 8f)
            {

                SinAmp += Time.deltaTime;

            }
            tf.position = new Vector3(tf.position.x,SinAmp * Mathf.Sin(YVel * SineFreq), tf.position.z);
    
        }
    }




   public void Roar()
    {

        FindObjectOfType<AudioManager>().Play("Roar");

    }



    public IEnumerator StartStuff()
    {
        started = false;
        StartCoroutine(FindObjectOfType<AudioManager>().FadeAudio("Level 1", 1f));

        yield return new WaitForSeconds(1f);

        FindObjectOfType<AudioManager>().Play("Here he is!");
        anim.SetTrigger("Intro");
        Intro = true;
    }

    public void Booming()
    {

        objectPooler.SpawnFromPool("BigExplosion", new Vector3(BoomPoint.position.x + Random.Range(-3.5f,3.5f), BoomPoint.position.y + Random.Range(-3.5f, 3.5f),0), Quaternion.identity);
        if (tf.position.x >= -15)
        {

            FindObjectOfType<AudioManager>().ChangePitch("Explosion", Random.Range(.1f, .75f));
            FindObjectOfType<AudioManager>().Play("Explosion");

        }


    }

    public void ShootRegular()
    {
        I = (-RegularAngle / 2) + 135;
        for (int i = 0; i < RegularAmount; i++)
        {

            I -= RegularAngle / RegularAmount;
            BulletRot = Quaternion.Euler(0, 0, I % 360);
            objectPooler.SpawnFromPool("EnemyBullet", MissleShotSpot.position, BulletRot);

        }

    }

    public void DieStart()
    {

        State = -1;
        XVel = 30f;

    }

    public void DieShake()
    {

        StartCoroutine(camerashake.Shake(5f, 1));

        FindObjectOfType<AudioManager>().Play("BossDeath");

        StartCoroutine(FindObjectOfType<AudioManager>().FadeAudio("Here he is!", 0.25f));

        paralaxStuff.paraspeedGoal = 0f;

        CoreAnim.SetTrigger("Broken");

        StartCoroutine(WaitThenDeactivate(6));


    }


    public IEnumerator WaitThenDeactivate(float Time)
    {
        FindObjectOfType<AudioManager>().Stop("Level 1");
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);
    }

    public void ShootMissle()
    {
        
        objectPooler.SpawnFromPool("Missle", MissleShotSpot.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Missle");
    }

    public void ShootTail()
    {

        objectPooler.SpawnFromPool("TailShot", MissleShotSpot.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().ChangePitch("Tail", Random.Range(1f, .75f));
        FindObjectOfType<AudioManager>().Play("Tail");
    }

    public void IntroDone()
    {

        Intro = false;

    }
}
