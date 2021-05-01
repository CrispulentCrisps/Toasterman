using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenDeactivate : MonoBehaviour
{
    private float T = 0f;
    private float inc;
    public float Max;
    private bool Spawned = false;
    ObjectPools objectPooler;

    void OnObjectSpawn()
    {
        T = 0;
        Spawned = true;
    }

    void Start()
    {
        inc = Time.deltaTime;
        T = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawned)
        {
            T += inc;
        }
        if (T >= Max && Spawned == true)
        {
            gameObject.SetActive(false);
            Spawned = false;
        }
    }
}
