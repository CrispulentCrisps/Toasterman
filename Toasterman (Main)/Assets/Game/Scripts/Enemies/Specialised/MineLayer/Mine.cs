using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IPooledObject
{
    protected ObjectPools objectPooler;
    private float T = 0;
    [SerializeField] private int Health;
    private int MaxHealth;
    [SerializeField] private float RotationAmount;
    [SerializeField] private float Max_t;
    [SerializeField] private int BulletAmount;
    [SerializeField] private int BulletArcAngle;
    [SerializeField] private string BulletName;
    [SerializeField] private string ExplosionName;
    [SerializeField] private string EnemyShootSound;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color HurtColour;
    [SerializeField] private string BrokenName;
    [SerializeField] private string[] CollisionNames;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        T = 0;
        EnemyScript.EnemyAmount++;
    }

    void Update()
    {
        sr.color += ((Color)Vector4.one * Time.deltaTime);
        Mathf.Clamp(sr.color.r, 0, Health / (float)MaxHealth);
        T += Time.deltaTime;

        if (Health <= 0)
        {
            objectPooler.SpawnFromPool(BrokenName, transform.position, Quaternion.identity);
            EnemyScript.EnemyAmount--;
            gameObject.active = false;
        }

        if (T > Max_t)
        {
            BulletPatternsModule.ShootArc(BulletArcAngle, BulletAmount, BulletName, transform, 0f);
            objectPooler.SpawnFromPool(ExplosionName, transform.position, Quaternion.identity);
            EnemyScript.EnemyAmount--;
            gameObject.active = false;
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, RotationAmount * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        for (int x = 0; x < CollisionNames.Length; x++)
        {
            if (coll.tag == CollisionNames[x])
            {
                Health -= (int)coll.GetComponent<DamageScript>().Damage;
                sr.color = HurtColour;
            }
        }
    }
}
