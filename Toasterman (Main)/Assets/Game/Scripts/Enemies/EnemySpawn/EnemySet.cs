using System.Collections;
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

    [Range(0f, 30)]
    public float EnemySpeed;

    [Range(0f, 5)]
    public float MinMax;

    [Range(0f,3)]
    public float Increment;
    [Header("Freq:WaveType 5")]
    [Range(-360, 360)]
    public float RotateSpeed;
    [Header("Amp:WaveType 5")]
    [Range(0f, 10f)]
    public float Radius;

    [Range(0f, 99)]
    public double Time;

    [Range(0.01f,5)]
    public float Spacing;

    [Range(1, 100)]
    public int Amount;

}
