using System.Collections;
using UnityEngine;

public class StemFlower : MonoBehaviour, IPooledObject
{
    protected ObjectPools objectPooler;

    [SerializeField]private float Health;
    [SerializeField]private float StartOffset;
    [SerializeField]private float TimeBetweenShots;
    [SerializeField]private int ArcSize;
    [SerializeField]private int BulletAmount;
    [SerializeField]private string BulletName;
    [SerializeField]private string BulletShootSound;
    [SerializeField]private string HurtSound;

    private Animator anim;
    private SpriteRenderer sr;
    private float Fire_t = 0;
    private bool Bloom = false;
    public bool Dead = false;
    public bool Detached = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartOffset = Random.Range(-2f, 2f);
        sr = GetComponent<SpriteRenderer>();
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        anim = GetComponent<Animator>();
        StartOffset = Random.Range(-2f, 2f);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) GetComponent<CircleCollider2D>().enabled = false;
        Dead = Health <= 0;
        sr.color += Color.white * Time.deltaTime;
        anim.SetBool("Bloomed", Bloom);
        if (Bloom)
        {
            Fire_t += Time.deltaTime;
            if (Fire_t > TimeBetweenShots && Health > 0)
            {
                anim.Play("StemFlowerShoot");
                Fire_t = -TimeBetweenShots;
            }
        }
    }

    public void BloomFlower()
    {
        if (gameObject.active)
        {
            StartCoroutine(BloomWait());
        }
    }

    IEnumerator BloomWait()
    {
        yield return new WaitForSeconds(StartOffset);
        Bloom = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Bullet")
        {
            sr.color = Color.red;
            Health -= coll.GetComponent<DamageScript>().Damage;
            if (Health <= 0)
            {
                Shooting.TargetScore += GetComponent<DamageScript>().Points * GetComponent<DamageScript>().PointMultiplier;
                anim.Play("StemFlowerDie");
            }
        }
    }

    public void DetachHead()
    {
        objectPooler.SpawnFromPool("FlowerHead", transform.position, Quaternion.identity);
        Detached = true;
    }
    public void BlowPetals()
    {
        objectPooler.SpawnFromPool("FlowerDie", transform.position, Quaternion.identity);
    }

    public void Fire()
    {
        Vector3 difference = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - transform.position;
        float AngleOffset = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - (0.5f * ArcSize);
        BulletPatternsModule.ShootArc(ArcSize, BulletAmount, BulletName, transform, AngleOffset-7.5f);
    }
}
