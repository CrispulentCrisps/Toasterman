using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public AncientAI ancientai;

    public SpriteRenderer rend;

    public Color HurtColour;

    private int Length;

    public double DamageMultiplier;

    private float CenteredRot;

    public string SoundName;
    public string[] CollisionNames;

    void Start()
    {
        Length = CollisionNames.Length;
    }

    void Update()
    {
        rend.color += new Color(255f / 255f, 255f / 255f, 255f / 255f) * Time.deltaTime * 5f;
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position,Vector3.up);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        for (int i = 0; i < Length; i++)
        {
            if (coll.gameObject.CompareTag(CollisionNames[i]) && HandScript.HandsGone == 2)
            {
                FindObjectOfType<AudioManager>().ChangePitch(SoundName, Random.Range(1f, 0.85f));
                FindObjectOfType<AudioManager>().Play(SoundName);
                ancientai.Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;
                rend.color = HurtColour;
            }
        }
    }
}
