using UnityEngine;

public class ScrapDamageScript : MonoBehaviour
{
    public SpriteRenderer[] sr;
    public string SoundName;

    private void Update()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color += new Color(1f, 0f, 0f, 255f) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (sr.Length > 0)
        {
            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = new Color(255f,0f,0f,255f);
            }
        }
        if (coll.gameObject.CompareTag("Bullet"))
        {
            ScrapBossAI.Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
            AudioManager.instance.Play(SoundName);
            AudioManager.instance.ChangePitch(SoundName, Random.Range(0.5f,1.5f));
        }
    }
}
