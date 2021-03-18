using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenDeactivate : MonoBehaviour
{
    private float T = 0f;
    private float inc;
    public float Max;

    void Start()
    {
        inc = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        T += inc;
        if (T >= Max)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
