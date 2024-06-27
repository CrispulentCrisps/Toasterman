using UnityEngine;

public class ScrapMiniMain : MonoBehaviour, IPooledObject
{
    public Transform ShootPos;
    public Animator Anim;
    float T = 0;
    bool IsFiring;
    public static float Health = 1000;
    private CameraShake camerashake;
    ObjectPools objectPooler;
    EnemyScript es;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
        camerashake = Camera.main.GetComponent<CameraShake>();
    }
    public void OnObjectSpawn()
    {
        Health = 1000;
        Anim.SetBool("Dead", false);
        transform.position = new Vector3(0, 1, 0);
        es = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();
        es.start = false;
    }

    private void Update()
    {
        Debug.Log(Health);
        if (Health < 0)
        {
            Anim.SetBool("Dead", true);
        }
        T += Time.deltaTime;
        if (T > .25f && IsFiring)
        {
            objectPooler.SpawnFromPool("ScrapyardGun", transform.position, Quaternion.identity);
            T = 0;
        }
    }

    public void ShootLargeSphereLeft()
    {
        objectPooler.SpawnFromPool("ScrapyardLargeBullet", ShootPos.position, Quaternion.Euler(0f, 0f, 0f));
        AudioManager.instance.Play("ScrapyardBulletlargeShoot");
    }

    public void ShootLargeSphereRight()
    {
        objectPooler.SpawnFromPool("ScrapyardLargeBullet", ShootPos.position, Quaternion.Euler(0f, 0f, 180f));
        AudioManager.instance.Play("ScrapyardBulletlargeShoot");
    }


    public void DecideAction()
    {
        if (Health > 0)
        {
            Anim.SetInteger("Descision", Random.Range(0, 3));
        }
    }

    public void StartFiring()
    {
        IsFiring = true;
    }
    public void StopFiring()
    {
        IsFiring = false;
    }
    public void Die()
    {
        es.start = true;
        gameObject.active = false;
    }

    public void FireyDeath()
    {
        StartCoroutine(camerashake.Shake(3.2f, .25f));
    }
}
