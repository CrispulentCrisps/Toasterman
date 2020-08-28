﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemySet
{
    public string EnemyName;

    [Range(1, 5)] // Wave 1 = straight, Wave 2 = Tri Wave, Wave 3 = Wall Wave, 4 = Circular Wave
    public int WaveType;

    [Range(-6,6)]
    public float StartYpos;

    [Range(0.1f, 30)]
    public float EnemySpeed;

    [Range(0.1f, 5)]
    public float MinMax;

    [Range(0.01f,3)]
    public float Increment;

    [Range(-360, 360)]
    public float RotateSpeed;

    [Range(0f, 10f)]
    public float Radius;

    [Range(0.1f, 99)]
    public double Time;

    [Range(0.01f,5)]
    public float Spacing;

    [Range(1, 100)]
    public int Amount;

}
