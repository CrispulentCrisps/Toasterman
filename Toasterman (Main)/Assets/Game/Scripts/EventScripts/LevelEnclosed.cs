using UnityEngine;
    
public class LevelEnclosed : MonoBehaviour, IPooledObject
{

    ObjectPools objectPooler;

    private Color LineColour = Color.white;
    private int Index;
    [HideInInspector] public float RealSpeed;
    [HideInInspector] public Vector2 LevelDir;
    public static bool Started;
    
    private float T;
    private float Preamble;
    public Vector3 PreviousPos;

    [System.Serializable]
    public class Point
    {
        public float Speed;
        public float TimeBetweenPoints;
        public Vector2 XY;
    }

    public Point[] Points;

    public void OnObjectSpawn()
    {
        Started = false;
        Preamble = 0;
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < Points.Length - 1; i++)
        {
            Gizmos.color = new Color(LineColour.r, LineColour.g / (i + 1), LineColour.b / (i + 1), 1f);
            Gizmos.DrawLine(new Vector2(Points[i+1].XY.x *-1, Points[i + 1].XY.y * -1), new Vector2(Points[i].XY.x * -1, Points[i].XY.y * -1));
        }
        Gizmos.DrawCube(transform.position, new Vector3(0f, 0f, 2f));
    }
    private void Update()
    {
        if (Started)
        {
            //If we get to the point
            if (transform.position.x == Points[Index].XY.x && transform.position.y == Points[Index].XY.y)
            {
                T += Time.deltaTime;
                if (T >= Points[Index].TimeBetweenPoints)
                {
                    T = 0;
                    Index++;
                    if (Points[Index].TimeBetweenPoints > 0)
                    {
                        RealSpeed = 0;
                    }
                }
            }
            else
            {
                //Keep moving towards the point
                RealSpeed += Points[Index].Speed * Time.deltaTime;
                RealSpeed = Mathf.Clamp(RealSpeed, 0, Points[Index].Speed);
                PreviousPos = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, Points[Index].XY, RealSpeed * Time.deltaTime);
                BulletAI.SpeedOffset = new Vector2((transform.position.x - PreviousPos.x) / Time.deltaTime, (transform.position.y - PreviousPos.y) / Time.deltaTime);
            }
        }
        else
        {
            Preamble += Time.deltaTime;
            if (Preamble > 5)
            {
                Started = true;
            }
        }
    }
}
