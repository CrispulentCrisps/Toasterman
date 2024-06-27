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

    [SerializeField] private float speedxRange;
    [SerializeField] private float speedyRange;

    [SerializeField] private float speeddiv;
    public void OnObjectSpawn()
    {
        speedx = Random.Range(speedxRange, speedxRange * 1.5f);
        speedy = Random.Range(-speedyRange, speedyRange);
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
        speedx *= speeddiv;
        speedy *= speeddiv;
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
