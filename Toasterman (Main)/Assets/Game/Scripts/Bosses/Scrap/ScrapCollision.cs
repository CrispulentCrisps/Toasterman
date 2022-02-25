using UnityEngine;

public class ScrapCollision : MonoBehaviour
{
    public ScrapBossAI sba;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            sba.health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
    }
}
