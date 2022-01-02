using System;
using System.Collections;
using UnityEngine;

public class TrutleBossAI : MonoBehaviour, IPooledObject
{

    public Transform tf;
    public Transform Target;
    public Transform MissleShotSpot;
    public Transform TailShotSpot;
    public Transform BoomPoint;

    public Rigidbody rb;

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

    public bool Intro = true;
    public bool Killed = false;
    private bool started = false;

    private float I; // regular shot angle

    public Vector3 Speed = new Vector3(25f,0f,0f);

    private float Timer;
    private float Timer2;
    private float Timer3;

    void Start()
    {
        objectPooler = ObjectPools.Instance;

        tf.position = new Vector3(25,0,-1);
        Timer = 0;
        Timer2 = 0;
        Timer3 = 0;

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
        Timer3 = 0;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        paralaxStuff = GameObject.FindGameObjectWithTag("BackGroundStuff").GetComponent<ParalaxStuff>();
        paralaxStuff.paraspeedGoal = 75f;
        BGAnim = GameObject.FindGameObjectWithTag("BGAnim").GetComponent<Animator>();
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
        //Death
        if (Health <= 0f && State != -1)
        {
            anim.SetTrigger("Death");
            State = -2;
            Timer = -1;
            Timer3 += Time.deltaTime;
            if (Timer3 >= 0.05f)
            {
                objectPooler.SpawnFromPool("Smoke", new Vector3(BoomPoint.position.x + UnityEngine.Random.Range(-3.5f, 3.5f), BoomPoint.position.y + UnityEngine.Random.Range(-1f, 1f), 0), Quaternion.identity);
                Timer3 = 0f;
            }
                        anim.SetTrigger("Death");
            if (tf.position.y > Target.position.y)
            {
                YSpeed = 5;
            }
            else if (tf.position.y < Target.position.y)
            {
                YSpeed = -5;
            }

        }
        else if (Health <= 0f && State == -1)
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

        if (Timer >= 4)
        {
            if (Health > 0f)
            {
                State = UnityEngine.Random.Range(0, 4);
            }
            if (Health > 0f)
            {
                switch (State) //Attacks
                {
                    case 0:
                        anim.SetTrigger("Idle");
                        break;
                    case 1:
                        anim.SetTrigger("Missle");
                        Timer = 0;
                        break;
                    case 2:
                        anim.SetTrigger("Tail");
                        Timer = 0;
                        break;
                    case 3:
                        anim.SetTrigger("BigHurt");
                        Timer = 0;
                        break;
                }
                Timer = 0;
            }
        }

        if (Health <= 400f && Health > 0)
        {
            paralaxStuff.paraspeedGoal = 100f;
            Timer2 += Time.deltaTime;
            if (Health > 300 && Timer2 >= 1f)
            {
                Booming();
                Timer2 = 0;
            }
            else if (Health > 200 && Health < 300 && Timer2 >= 0.75f)
            {
                Booming();
                Timer2 = 0;
            }
            else if (Health > 100 && Health < 200 && Timer2 >= 0.5f)
            {
                Booming();
                Timer2 = 0;
            }
            else if (Health < 100 && Timer2 >= 0.25f)
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
            if (SinAmp <= 7f)
            {
                SinAmp += Time.deltaTime;
            }
            tf.position = new Vector3(7f, SinAmp * Mathf.Sin(YVel * SineFreq), tf.position.z);
        }
    }

   public void Roar()
    {
        AudioManager.instance.Play("Roar");
    }

    public IEnumerator StartStuff()
    {
        started = false;
        StartCoroutine(AudioManager.instance.FadeAudio("Level 1", 1f));

        yield return new WaitForSeconds(1f);

        AudioManager.instance.Play("Here he is!");
        anim.SetTrigger("Intro");
        Intro = true;
    }

    public void Booming()
    {
        objectPooler.SpawnFromPool("BigExplosion", new Vector3(BoomPoint.position.x + UnityEngine.Random.Range(-3.5f,3.5f), BoomPoint.position.y + UnityEngine.Random.Range(-3.5f, 3.5f),0), Quaternion.identity);
        if (tf.position.x >= -15)
        {
            AudioManager.instance.ChangePitch("Explosion", UnityEngine.Random.Range(.1f, 1f));
            AudioManager.instance.Play("Explosion");
        }
    }

    public void ShootRegular()
    {
        BulletPatternsModule.ShootArc(RegularAngle,RegularAmount,"EnemyBullet",MissleShotSpot,90f);
    }

    public void DieStart()
    {
        State = -1;
        XVel = 30f;
    }

    public void DieShake()
    {
        StartCoroutine(camerashake.Shake(5f, 1));
        AudioManager.instance.Play("BossDeath");
        StartCoroutine(AudioManager.instance.FadeAudio("Here he is!", 0.25f));
        paralaxStuff.paraspeedGoal = 0f;
        CoreAnim.SetTrigger("Broken");
        StartCoroutine(WaitThenDeactivate(6));
    }


    public IEnumerator WaitThenDeactivate(float Time)
    {
        AudioManager.instance.Stop("Level 1");
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);
    }

    public void ShootMissle()
    {  
        objectPooler.SpawnFromPool("Missle", MissleShotSpot.position, Quaternion.identity);
        AudioManager.instance.Play("Missle");
    }

    public void ShootTail()
    {
        for (int i = 0; i < 2; i++)
        {
            objectPooler.SpawnFromPool("TailShot", MissleShotSpot.position, Quaternion.identity);
            AudioManager.instance.ChangePitch("Tail", UnityEngine.Random.Range(1f, .75f));
            AudioManager.instance.Play("Tail");
        }
    }

    public void IntroDone()
    {
        Intro = false;
    }

    public void ResetTriggers()
    {
        anim.ResetTrigger("Missle");
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Tail");
        anim.ResetTrigger("BigHurt");
    }
}
