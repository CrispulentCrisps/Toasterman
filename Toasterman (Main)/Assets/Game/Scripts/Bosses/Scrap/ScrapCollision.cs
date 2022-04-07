using UnityEngine;

public class ScrapCollision : MonoBehaviour
{
    public ScrapBossAI sba;

	public void Start()
	{
        sba = GameObject.Find("ScrapBoss").GetComponent<Animator>().GetBehaviour<ScrapBossAI>();
	}
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            sba.health -= coll.gameObject.GetComponent<DamageScript>().Damage;
        }
    }
}
