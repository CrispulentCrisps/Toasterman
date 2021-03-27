using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenDeactivate : MonoBehaviour
{
    private float T = 0f;
    private float inc;
    public float Max;

    ObjectPools objectPooler;

    void OnObjectSpawn()
    {
        T = 0;
    }

    void Start()
    {
        inc = Time.deltaTime;
        T = 0;
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
