using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class LevelEnclosed : MonoBehaviour
{
    [SerializeField]private Vector2[] Points;
    private Vector2[] VisualisePoints;
    private int Index;
    [SerializeField] private float Speed;
    [SerializeField] private float TimeBetweenPoints;
    private float T;
    private void Awake()
    {
        VisualisePoints = new Vector2[Points.Length];
        for (int i = 0; i < VisualisePoints.Length; i++)
        {
            VisualisePoints[i] = Points[i];
        }
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < Points.Length - 1; i++)
        {
            Gizmos.DrawLine(Points[i], Points[i + 1]);
        }
        Gizmos.DrawCube(transform.position, new Vector3(2f, 2f, 2f));
    }
    private void Update()
    {
        if (transform.position.x == Points[Index].x && transform.position.y == Points[Index].y)
        {
            T += Time.deltaTime;
            if (T >= TimeBetweenPoints)
            {
                T = 0;
                Index++;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[Index], Speed * Time.deltaTime);
        }
    }
}
