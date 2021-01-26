using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoomScript : MonoBehaviour, IPooledObject
{

    public float timer = 0f;
    public float Incement;
    public float Max;

    private bool played = false;

    public void OnObjectSpawn()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (played == false)
        {
            gameObject.GetComponent<ParticleSystem>().enableEmission = true;
            played = true;
        }else if (gameObject.activeSelf == true)
        {
            timer += Incement * Time.deltaTime;
            if (timer >= Max)
            {
                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }
}
