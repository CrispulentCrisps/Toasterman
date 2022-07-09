using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject RoamingMonster;

    public Light2D light;

    Transform MonsterTrans;

    float Wait;
    float Max;
    float Intensity;
    float MonsterChance;

    private void Start()
    {
        MonsterChance = Random.Range(0,100);
        Wait = 0;
        Intensity = Random.Range(5f, 40f);
        Max = Random.Range(1f, 7f);
        light.intensity = Intensity;
        MonsterTrans = RoamingMonster.GetComponent<Transform>();
    }

    void Update()
    {
        Wait += Time.deltaTime;
        if (Wait >= Max) 
        {
            if (MonsterChance >= 99 && !RoamingMonster.active)
            {
                RoamingMonster.SetActive(true);
                MonsterTrans.position = new Vector3(15f, 4f, 5f);
            }
            else if (MonsterTrans.position.x < -21f)
            {
                RoamingMonster.SetActive(false);
            }
            
            Wait = 0;
            Intensity = Random.Range(5f, 40f);
            Max = Random.Range(1f, 7f);
            light.intensity = Intensity;
            MonsterChance = Random.Range(0, 100);
        }
        light.intensity -= 20f * Time.deltaTime;
        if (light.intensity < 1f)
        {
            light.intensity = 1;
        }
    }
}
