using UnityEngine;

public class CircleAroundPoint : MonoBehaviour
{
    public Transform Tf;
    public Transform CenterPoint;
    public Rigidbody2D rb;
    public float XAmp;
    public float YAmp;
    public float Speed;
    public float PhaseOffset;
    private float TT;//TotalTime

    public bool PointFromCenter;

    // Update is called once per frame
    void FixedUpdate()
    {
        TT += Time.deltaTime * Speed;
        //rb.position = new Vector3(CenterPoint.position.x + (XAmp * Mathf.Sin(TT + PhaseOffset)) + (XAmp * 0.5f), CenterPoint.position.y + (YAmp * Mathf.Cos(TT + PhaseOffset)) + (YAmp * 0.5f), 0f);
        rb.position = new Vector3(CenterPoint.position.x + (XAmp * Mathf.Sin(TT + PhaseOffset)), CenterPoint.position.y + (YAmp * Mathf.Cos(TT + PhaseOffset)), 0f);

        if (PointFromCenter)
        {
            Vector3 difference = CenterPoint.position - Tf.position;
            //Tf.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
            rb.rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
    }
}
