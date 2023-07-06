using UnityEngine;

public class CircleAroundPoint : MonoBehaviour
{
    public Transform Tf;
    public Transform CenterPoint;
    public Rigidbody2D rb;
    public float XAmp;
    public float YAmp;
    public float ZAmp;
    public float XOff;
    public float YOff;
    public float ZOff;
    public float Speed;
    public float PhaseOffset;
    private float TT;//TotalTime

    public bool PointFromCenter;

    // Update is called once per frame
    void FixedUpdate()
    {
        TT += Time.deltaTime * Speed;

        if (PointFromCenter)
        {
            Vector3 difference = CenterPoint.position - Tf.position;
            rb.rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        rb.position = new Vector3(CenterPoint.position.x + (XAmp * Mathf.Sin(TT + PhaseOffset) + XOff), CenterPoint.position.y + (YAmp * Mathf.Cos(TT + PhaseOffset) + YOff), CenterPoint.position.z + (ZAmp * Mathf.Sin(TT + PhaseOffset) + ZOff));
    }
}
