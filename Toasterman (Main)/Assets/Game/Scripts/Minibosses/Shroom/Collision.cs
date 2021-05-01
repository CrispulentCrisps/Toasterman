using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public SpriteRenderer renderer;

    public Color hurtcolour;

    void Update()
    {
        renderer.color += new Color(1f, 1f, 1f) * Time.deltaTime * 5f;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            ShroomMiniAI.Health -= col.GetComponent<DamageScript>().Damage;
            renderer.color = hurtcolour;
        }
    }
}
