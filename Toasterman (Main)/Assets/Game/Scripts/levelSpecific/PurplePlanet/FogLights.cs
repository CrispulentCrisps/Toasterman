using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class FogLights : MonoBehaviour
{
    Light2D light;
    EnemyScript es;
    private void Start()
    {
        light = GetComponent<Light2D>();
        es = GameObject.FindGameObjectWithTag("Wave spawner").GetComponent<EnemyScript>();
        light.pointLightOuterRadius = 0;
    }

    private void FixedUpdate()
    {
        if (es.i >= 19)
        {
            if (light.pointLightOuterRadius > 3)
            {
                light.pointLightOuterRadius = 3;
            }
            else
            {
                light.pointLightOuterRadius += Time.deltaTime;
            }
        }
    }
}
