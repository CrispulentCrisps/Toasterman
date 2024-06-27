using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackNForth : MonoBehaviour
{
    [SerializeField] private Vector2[] Points;
    [SerializeField] private Vector2 StartingPoint;
    private Vector2[] VisualisePoints;
    private int Index;
    [SerializeField] private float Speed;
    private float T;
    private void Awake()
    {
        VisualisePoints = new Vector2[Points.Length];
        for (int i = 0; i < VisualisePoints.Length; i++)
        {
            VisualisePoints[i] = Points[i];
        }
        StartingPoint = transform.position;
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = Points[i] + StartingPoint;
        }
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < Points.Length - 1; i++)
        {
            Gizmos.DrawLine(Points[i], Points[i + 1]);
        }
    }
    private void Update()
    {
        if (transform.position.x == Points[Index].x && transform.position.y == Points[Index].y)
        {
            T += Time.deltaTime;

            T = 0;
            Index++;
            if (Index > Points.Length)
            {
                Index = 0;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[Index], Speed * Time.deltaTime);
        }
    }
}
