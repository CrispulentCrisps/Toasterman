using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform[] Rings;
    public Rigidbody2D[] Rb;
    public float RotSpeed;
    private Vector2 ThrownMovement;
    private int ThrowIndex;
    private int State;
    private float t;
    private bool RingThrown;
    private bool Shot = false;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (State == 0)
        {
            if (t >= 4f && !RingThrown)
            {
                ThrowIndex = Random.RandomRange(0, 7);
                RingThrown = true;
                t = 0;
                ThrownMovement = new Vector2(-9.81f * 2f, 0f);
            }

            for (int i = 0; i < Rings.Length; i++)
            {
                int j = i + 1;
                if (j % 2 == 0)
                {
                    j *= -1;
                }
                Rings[i].Rotate(new Vector3(0f, 0f, RotSpeed / j * Time.deltaTime));
            }

            if (RingThrown)
            {
                ThrownMovement += new Vector2(9.81f, 0f) * Time.deltaTime;
            }

            if (!Shot && ThrownMovement.x > -.1f || !Shot && ThrownMovement.x < .1f)
            {
                if (RingThrown && Rings[ThrowIndex].position.x < -10f)
                {
                    StartCoroutine(BulletPatternsModule.ShootArcEnum(360f, 32, "SmallRock", Rings[ThrowIndex], 0f, .05f));
                    StartCoroutine(BulletPatternsModule.ShootArcEnum(360f, 32, "SmallRock", Rings[ThrowIndex], 180f, .05f));
                    Shot = true;
                }
            }

            Rings[ThrowIndex].position += (Vector3)ThrownMovement * Time.deltaTime;

            if (Rings[ThrowIndex].position.x > 9f)
            {
                Shot = false;
                RingThrown = false;
                Rings[ThrowIndex].position = new Vector3(9f, Rings[ThrowIndex].position.y, Rings[ThrowIndex].position.z);
            }
        }
    }
}
