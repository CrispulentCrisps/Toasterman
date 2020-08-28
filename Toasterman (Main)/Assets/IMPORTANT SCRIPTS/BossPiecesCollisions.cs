using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPiecesCollisions : MonoBehaviour
{

    public TrutleBossAI TurtleBossAI;

    public string SoundName;

    public string[] CollisionNames;

    public double DamageMultiplier;

    private int Length;

    void OnTriggerEnter2D(Collider2D coll)
    {

        Length = CollisionNames.Length;

        for (int i = 0; i < Length; i++)
        {
            if (coll.gameObject.CompareTag(CollisionNames[i]))
            {

                FindObjectOfType<AudioManager>().ChangePitch(SoundName, Random.Range(1f, 0.5f));
                FindObjectOfType<AudioManager>().Play(SoundName);

                TurtleBossAI.Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;


            }
        }
    }
}
