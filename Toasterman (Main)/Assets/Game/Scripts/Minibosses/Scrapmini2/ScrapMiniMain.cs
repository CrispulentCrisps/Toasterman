using UnityEngine;

public class ScrapMiniMain : MonoBehaviour, IPooledObject
{
    public Transform ShootPos;
    public Animator Anim;
    float T = 0;
    bool IsFiring;

    ObjectPools objectPooler;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }
    public void OnObjectSpawn()
    {

    }

    private void Update()
    {
        T += Time.deltaTime;
        if (T > .5f && IsFiring)
        {
            objectPooler.SpawnFromPool("ScrapyardGun", transform.position, Quaternion.identity);
            T = 0;
        }
    }

    public void ShootLargeSphereLeft()
    {
        objectPooler.SpawnFromPool("ScrapyardLargeBullet", ShootPos.position, Quaternion.Euler(0f, 0f, 0f));
    }

    public void ShootLargeSphereRight()
    {
        objectPooler.SpawnFromPool("ScrapyardLargeBullet", ShootPos.position, Quaternion.Euler(0f, 0f, 180f));
    }


    public void DecideAction()
    {
        Anim.SetInteger("Descision", Random.RandomRange(2, 2));
    }

    public void StartFiring()
    {
        IsFiring = true;
    }
    public void StopFiring()
    {
        IsFiring = false;
    }
}
