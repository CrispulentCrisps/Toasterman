using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollision : MonoBehaviour
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color += new Color(10f, 10f, 10f, 10f) * Time.deltaTime;

        if (GetComponentInParent<TurretAI>().Health <= 0f)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            sr.color = Color.red;
            AudioManager.instance.ChangePitch("MetalHit", Random.Range(.75f, 1.25f));
            AudioManager.instance.Play("MetalHit");
            GetComponentInParent<TurretAI>().Health -= collision.GetComponent<DamageScript>().Damage;
        }
    }
}
