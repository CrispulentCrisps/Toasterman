using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDamage : MonoBehaviour
{
    public DragonAI DAI;
    public SpriteRenderer Sr;

    private void Update()
    {
        Sr.color += new Color(5f, 5f, 5f) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Sr.color = new Color(255f, 0f, 0f);
            DAI.Health -= (int)coll.GetComponent<DamageScript>().Damage;
        }
    }
}
