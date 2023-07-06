using UnityEngine;

public class ScrapCollision : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            ScrapBossAI.health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
    }
}
