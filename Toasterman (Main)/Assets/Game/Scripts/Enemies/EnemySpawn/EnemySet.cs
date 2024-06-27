using UnityEngine;

[System.Serializable]

public class EnemySet
{
    public enum XorY
    {
        X = 0, Y = 1,
    }

    public string EnemyName;

    [Range(1, 5)] // Wave 1 = straight, Wave 2 = Tri Wave, Wave 3 = Wall Wave, 4 = Circular Wave
    public int WaveType;

    [Range(-12,12)]
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

    [Range(0.01f,10)]
    public float Spacing;

    [Range(1, 100)]
    public int Amount;

    [Header("Only use this for HelperHat")]
    [Range(0, 5)]
    public int PowerType;
    [Range(-1,1)]
    [Header("Reverses values of attributes and sets enemies to the left of the screen")]
    public int Inverse;
    [Header("Determines if the enemy is going on the Y axis or the X")]
    public XorY XY;
}
