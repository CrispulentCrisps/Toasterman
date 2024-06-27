using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapHurt : MonoBehaviour
{
    SpriteRenderer sr;
    public string EnemyHurtSound;
    public Color hurtColour;
    public float DamageMult;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color += new Color(1f, 1f, 1f) * Time.deltaTime;
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
            ScrapMiniMain.Health -= coll.GetComponent<DamageScript>().Damage;
            sr.color = hurtColour;
        }
    }
}
