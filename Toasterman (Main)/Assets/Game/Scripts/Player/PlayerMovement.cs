﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IPooledObject
{
    public Transform tf;

    public BoxCollider2D bounds;

    private float Health = 100f;
    private float TargetHealth = 100f;
    public float RegenRate;

    private float Velocity = 10;
    public float Normalspeed;

    private float timer; // Wait before the regen starts
    private float Timer2;// Smoke timer
    private float TrailTimer;// Dash trail timer

    private float DashTimer;
    public float DashTimerSpeed;

    public float DashLength;
    public float DashLengthRet;
    public float DashSpeed;

    public bool Dashin = false;
    private bool Invincible = false;
    private bool Alive = true;
    public bool Inverse;

    ObjectPools objectPooler;

    public CameraShake camerashake;

    public Slider HealthSlider;
    public Slider DashSlider;

    public Animator DashAnim;
    public Animator HealthAnim;
    public Animator Anim;

    public Vector3 Movement;

    // Start is called before the first frame update
    void Start()
    {
        Movement = new Vector3(0,0,0);
        timer = 0f;
        Timer2 = 0.5f;
        DashTimer = 0f;
        TrailTimer = 0;
        objectPooler = ObjectPools.Instance;
        DashAnim = DashSlider.GetComponent<Animator>();
        //Blast = true;
    }

    public void OnObjectSpawn()
    {
        Movement = new Vector3(0, 0, 0);
        timer = 0f;
        Timer2 = 0.5f;
        DashTimer = 0f;
        TrailTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("NotSoGoodThing") && Dashin == false && coll.gameObject.GetComponent<DamageScript>().Damage > 0f && Invincible == false)
        {
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
            HealthAnim.SetTrigger("HealthDown");
            StartCoroutine(camerashake.Shake(0.1f, 0.1f));
            StartCoroutine(camerashake.AbberationChange(1f, 1f));
            Anim.SetTrigger("Hurt");
            timer = 5f;
            AudioManager.instance.Play("Hurt");
            AudioManager.instance.ChangePitch("Hurt",Random.Range(0.75f,1.25f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = Health / 100f;//health slider
        //Checks movement debuffs
        if (Inverse)
        {
            Movement.x = Input.GetAxis("Horizontal") * -Velocity;
            Movement.y = Input.GetAxis("Vertical") * -Velocity;
        }
        else
        {
            Movement.x = Input.GetAxis("Horizontal") * Velocity;
            Movement.y = Input.GetAxis("Vertical") * Velocity;
        }

        if (Alive == true)
        {
            DashSlider.value += DashTimerSpeed * Time.deltaTime;//dash
            timer -= 1f * Time.deltaTime;//shooting
            Timer2 -= 2f * Time.deltaTime;//smoke
            if (Alive && !Invincible)//This makes sure that player takes damage in hitbox
            {
                bounds.enabled = !bounds.enabled;
            }
            else if (Invincible)
            {
                TrailTimer += Time.deltaTime;
                if (TrailTimer >= 0.0125f)
                {
                    DashTrail();
                    TrailTimer = 0f;
                }
            }

            if (HealthSlider.value <= 0)
            {
                Die();
            }
            //Dashing
            if (Input.GetKeyDown(KeyCode.Space) && DashSlider.value >= 1f)
            {
                Dash();
            }
            else if (DashLength > 0)
            {
                DashLength -= 1 * Time.deltaTime;
            }
            else if (DashLength <= 0f && Dashin == true)
            {
                Velocity = Normalspeed;
                Dashin = false;
            }
            //Smoke
            if (Health <= TargetHealth / 3)
            {
                SmokeHeavy();
            }
            else if (Health <= TargetHealth / 1.5)
            {
                SmokeLight();
            }

            if (Health < TargetHealth && timer <= 0)
            {
                Health += RegenRate;
            }
            else if (Health > TargetHealth)
            {
                Health -= RegenRate;
            }

            if (DashSlider.value == 1f)
            {
                DashAnim.Play("DashFull");
            }
        }
    }

    void FixedUpdate()
    { 
        tf.Translate(Movement * Time.deltaTime);
    }
    //-------------------------------------------------|=========|-------------------------------------------------------\\
    //-------------------------------------------------|==functions= |-------------------------------------------------------\\
    //-------------------------------------------------|=========|-------------------------------------------------------\\
    public void Die()
    {
        objectPooler.SpawnFromPool("PlayerBlast", tf.position, Quaternion.identity);
        AudioManager.instance.Play("BigExplosion");
        Alive = false;
        Anim.SetTrigger("Killed");
        StartCoroutine(camerashake.Shake(1f, 1f));
        tf.position = new Vector3(0, 0, 0);
        for (int i = 0; i < Shooting.BulletType; i++)
        {
            Shooting.BulletLevel[i] = 0;
        }
    }

    public void invincible()
    {
        Invincible = true;
    }

    public void Uninvincible()
    {
        Invincible = false;
    }

    public void alive()
    {
        Health = 100;
        Alive = true;
    }

    public void DashTrail()
    {
        objectPooler.SpawnFromPool("DashTrail", tf.position, Quaternion.identity);
    }

    public void Dash()
    {
        DashSlider.value = 0f;
        DashLength = DashLengthRet;
        DashTimer = DashLength;
        Velocity = DashSpeed;
        Anim.SetTrigger("Dash");
        AudioManager.instance.Play("Dash");
        Dashin = true;
    }

    public void SmokeLight()
    {
        if (Timer2 <= 0)
        { 
            objectPooler.SpawnFromPool("LightSmoke", tf.position, Quaternion.identity);
            Timer2 = 0.25f;
        }
    }

    public void SmokeHeavy()
    {
        if (Timer2 <= 0)
        {
            objectPooler.SpawnFromPool("Smoke", tf.position, Quaternion.identity);
            Timer2 = 0.25f;
        }
    }
}
