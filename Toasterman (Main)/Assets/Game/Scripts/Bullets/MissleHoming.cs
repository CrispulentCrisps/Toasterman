using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleHoming : MonoBehaviour, IPooledObject
{

    public Transform Target;
    public CameraShake cameraShake;
    public Transform tf;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    ObjectPools objectPooler;

    public float Health;

    public float speed;
    private float Rotspeed;
    public float RotVel;
    public float RotMaxSpeed;
    public float StartRot;
    private float timer;
    public float MaxTime;

    public bool IsEnemy;
    public bool Targeting;

    public string TargetTag;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        objectPooler = ObjectPools.Instance;

        try//If there is not target on screen
        {
            Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
            Debug.Log("!!! TARGET FOUND !!!");
        }
        catch (NullReferenceException e) when (e != null)
        {
            Debug.Log("NO TARGET FOUND");
            Target = tf;
        }
    }

    public void OnObjectSpawn()
    {
        if (sr == null)
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
        }

        try//If there is not target on screen
        {
            Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
            Debug.Log("!!! TARGET FOUND !!!");
        }
        catch (NullReferenceException e) when (e != null)
        {
            Debug.Log("NO TARGET FOUND");
            Target = tf;
        }

        tf.Rotate(0, 0, StartRot);

        timer = 0;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet") && IsEnemy)
        {
            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
        else if (coll.gameObject.CompareTag(TargetTag))
        {
            StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
            objectPooler.SpawnFromPool("BigExplosion", tf.position, Quaternion.identity);
            AudioManager.instance.ChangePitch("Explosion", UnityEngine.Random.Range(.1f, .75f));
            AudioManager.instance.Play("Explosion");
            gameObject.SetActive(false);
            timer = 0;
            Rotspeed = 0;
            Health = 5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Lifespan
        timer += Time.deltaTime;
        if (timer <= 0.5f || timer >= (MaxTime - 0.5f))
        {
            Targeting = false;
        }
        else
        {
            Targeting = true;
        }

        if (Target && Targeting)
        {
            Vector2 Direction = (Vector2)Target.position - rb.position;

            Direction.Normalize();

            float RotateAmount = Vector3.Cross(Direction, transform.up).z;

            rb.angularVelocity = -RotateAmount * Rotspeed;

        }

        if (Rotspeed <= RotMaxSpeed && Targeting)
        {
            Rotspeed += RotVel;
        }

        rb.velocity = transform.up * speed;
        sr.color = new Color(255f, 255 * Mathf.Cos(timer * timer * 10f), 255 * Mathf.Cos(timer * timer * 10f), 255f);
        if (timer >= MaxTime || Health <= 0f)
        {
            objectPooler.SpawnFromPool("BigExplosion", tf.position, Quaternion.identity);
            AudioManager.instance.ChangePitch("Explosion", UnityEngine.Random.Range(.1f, 1f));
            AudioManager.instance.Play("Explosion");
            gameObject.SetActive(false);
            timer = 0;
            Rotspeed = 0;
            Health = 10f;
        }
    }
}
