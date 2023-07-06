using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    private SpriteRenderer Sr;

    public float Health;

    public string EnemyHurtSound;
    public string ExplosionName;
    public string ExplosionSound;

    ObjectPools objectPooler;

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Sr = GetComponent<SpriteRenderer>();
        objectPooler = ObjectPools.Instance; 
        if (ExplosionName == "")
        {
            ExplosionName = "Boom";
        }
        if (ExplosionSound == "")
        {
            ExplosionSound = "SmallExplosion";
        }
    }

    private void Update()
    {
        Sr.color += new Color(5f,5f,5f) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            if (EnemyHurtSound != "")
            {
                AudioManager.instance.ChangePitch(EnemyHurtSound, Random.Range(0.75f, 1.25f));
                AudioManager.instance.Play(EnemyHurtSound);
            }
            Health -= coll.GetComponent<DamageScript>().Damage;
            Sr.color = Color.red;
            if (Health <= 0f)
            {
                StartCoroutine(SpawnExplosions());
            }
        }
    }
    public IEnumerator SpawnExplosions()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < transform.localScale.y; i++)
        {

            objectPooler.SpawnFromPool(ExplosionName, new Vector3(transform.position.x, transform.position.y + i, 0f), Quaternion.identity);
            objectPooler.SpawnFromPool(ExplosionName, new Vector3(transform.position.x, transform.position.y - i, 0f), Quaternion.identity);
            if (ExplosionSound != "")
                {
                    AudioManager.instance.Play(ExplosionSound);
                }

            yield return new WaitForSeconds(0.025f);
        }

        gameObject.SetActive(false);
    }
}
