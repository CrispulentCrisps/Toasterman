using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollisions : MonoBehaviour
{
    public SpriteRenderer renderer;

    public BoxCollider2D bounds;

    public Color hurtcolour;

    private float ST;
    private float Samp = 1f;
    private float Freq = 1f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            RobotRunToPlayer.Health -= coll.GetComponent<DamageScript>().Damage;
            renderer.color = hurtcolour;
        }
    }

    public void Update()
    {
        if (RobotRunToPlayer.Health > 0f)
        {
            renderer.color += new Color(1f, 1f, 1f) * Time.deltaTime * 5f;
        }
        if (RobotRunToPlayer.Health <= 0f)
        {
            bounds.enabled = false;
            ST += Time.deltaTime;
            Freq += Time.deltaTime;
            renderer.color = new Color(Samp * Mathf.Sin(ST * Freq) * 255f, Samp * Mathf.Sin(ST * Freq * 4f) * 255f, Samp * Mathf.Sin(ST * Freq * 4f) * 255f,255f) * Time.deltaTime;
        }
    }
}
