using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBullet : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform tf;
    SpriteRenderer Sr;
    private Vector2 Movement;

    private float speedx = 0;
    private float speedy = 0;

    public void OnObjectSpawn()
    {
        speedx = Random.Range(30f, 45f);
        speedy = Random.Range(-15f, 15f);
        tf.localScale = new Vector3(1f, 1f, 1f);
        Sr = gameObject.GetComponent<SpriteRenderer>();
        Sr.color = new Color(255f, 255f, 255f, 255f);
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        Sr.color = new Color(255f, 255f, 255f, speedx * 255f - 25.5f) * Time.deltaTime;
        speedx *= 0.92f;
        speedy *= 0.92f;
        Movement = new Vector2(speedx, speedy);
        tf.localScale += new Vector3(1f, 1f, 0f) * Time.deltaTime;
        if (speedx <= 0.1f)
        {
            speedx = 0f;
            speedy = 0f;
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        tf.Translate(Movement * Time.deltaTime);
    }
}
