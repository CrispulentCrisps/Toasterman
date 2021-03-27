using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapDamageScript : MonoBehaviour
{
    public string SoundName;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            ScrapBossAI.Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
            AudioManager.instance.Play(SoundName);
            AudioManager.instance.ChangePitch(SoundName, Random.Range(0.5f,1.5f));
        }
    }
}
