using UnityEngine;

public class HandScript : MonoBehaviour
{
    public AncientAI ancientai;

    public Transform tf;
    public Transform EyePos;

    public Animator anim;

    public static int HandsGone = 0;
    private int Length;

    public float Health;

    public double DamageMultiplier;
    
    public string SoundName;
    public string[] CollisionNames;

    public bool DoneHands = false;
    private bool ToEye = false;

    void Start()
    {
        Health = ancientai.Health * 0.5f;
        Length = CollisionNames.Length;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        for (int i = 0; i < Length; i++)
        {
            if (coll.gameObject.CompareTag(CollisionNames[i]))
            {
                AudioManager.instance.ChangePitch(SoundName, Random.Range(1f, 0.85f));
                AudioManager.instance.Play(SoundName);
                Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;
                if (Health > 0f)
                {
                    anim.SetTrigger("Hurt");
                }
            }
        }

        if (Health <= 0f && DoneHands == false)
        {
            HandsGone++;
            anim.SetTrigger("Dead");
            DoneHands = true;
        }
    }

    void LateUpdate()
    {
        if (ToEye == true)
        {
            tf.position = EyePos.position;
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }


    public void getToEye()
    {
        ToEye = true;
    }
}
