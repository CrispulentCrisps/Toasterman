using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShooterScript : MonoBehaviour, IPooledObject
{
    public Transform tf;
    public Transform Target;

    public GameObject Ship;
    public Shooting shooting;

    ObjectPools objectPooler;

    public float angle;
    public float Radius;
    public float RotateSpeed;

    public float MinTime;
    private float CurrentTime;

    public static int AmountOfSpinnys;

    public void OnObjectSpawn()
    {
        Ship = GameObject.Find("Ship");//gets the game object
        Target = Ship.GetComponent<Transform>();// gets the Transform 
        shooting = Ship.GetComponent<Shooting>();// gets the Shooting script
        //https://answers.unity.com/questions/1068513/place-8-objects-around-a-target-gameobject.html thank you ^>^
        angle = AmountOfSpinnys * Mathf.PI;
        Radius = 0f;
        MinTime = 0f;
        AmountOfSpinnys++;
    }

    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        angle += RotateSpeed * Time.deltaTime;

        tf.Rotate(0f,0f,RotateSpeed * 45f * Time.deltaTime);

        if (Radius < 2f)
        {
            Radius += Time.deltaTime;
        }

        CurrentTime += Time.deltaTime;
        MinTime = shooting.ReloadTime * 2f;

        if (Target != null && shooting != null)
        {
            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
            tf.position = (Vector2)Target.position + offset;

            if (shooting.Auto && CurrentTime >= MinTime)// shooting input
            {
                shooting.Shoot(tf);
                CurrentTime = 0f;
            }
        }
    }

}
