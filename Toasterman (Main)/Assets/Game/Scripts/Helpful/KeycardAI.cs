using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardAI : MonoBehaviour
{
    public GameObject Door;
    Transform Img;
    Vector2 pos;
    float p;

    private void Start()
    {
        Img = GetComponentInChildren<Transform>();
        pos = Img.position;
    }

    private void Update()
    {
        p += Time.deltaTime * 3f;
        Img.position = new Vector2(pos.x, pos.y + Mathf.Sin(p)*.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("ENTERED");
        if (other.tag == "Player")
        {
            Door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
