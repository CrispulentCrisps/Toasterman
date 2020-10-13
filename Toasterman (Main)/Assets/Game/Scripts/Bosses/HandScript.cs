using UnityEngine;

public class HandScript : MonoBehaviour
{
    public AncientAI ancientai;

    public static int HandsGone = 0;

    public Animator anim;

    public float Health;

    public string SoundName;

    public string[] CollisionNames;

    public double DamageMultiplier;

    private int Length;

    void Start()
    {
        Health = ancientai.Health * 0.33f;
        Length = CollisionNames.Length;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        for (int i = 0; i < Length; i++)
        {
            if (coll.gameObject.CompareTag(CollisionNames[i]))
            {
                FindObjectOfType<AudioManager>().ChangePitch(SoundName, Random.Range(1f, 0.5f));
                FindObjectOfType<AudioManager>().Play(SoundName);
                ancientai.Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;
                Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;
            }
        }
        if (Health <= 0f)
        {
            HandsGone++;
            anim.Play("Dead");
        }
    }
}
