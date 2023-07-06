using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDragon : MonoBehaviour
{
    public Vector3 Movement;
    public float Speed;
    private float T;
    void Update()
    {
        T += Time.deltaTime;
        if (T > 10)
        {
            int Choice = Random.Range(0, 100);
            if (Choice >= 1)
            {
                Movement.x = Speed;
            }
            else if(transform.position.x > 40)
            {
                transform.position = new Vector3(40f, -90f, 0f);
                transform.position = new Vector3(-20f, -90f, 0f);
                transform.position = new Vector3(-20f, -5f, 0f);
                Movement.x = 0f;
            }
            T = 0;
        }
        transform.Translate(Movement * Time.deltaTime);
    }
}
