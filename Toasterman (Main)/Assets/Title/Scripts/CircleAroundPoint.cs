using UnityEngine;

public class CircleAroundPoint : MonoBehaviour
{
    public Transform Tf;
    public Transform CenterPoint;
    public Rigidbody2D rb;
    public float XAmp;
    public float YAmp;
    public float XOff;
    public float YOff;
    public float Speed;
    public float PhaseOffset;
    private float TT;//TotalTime

    public bool PointFromCenter;

    // Update is called once per frame
    void Update()
    {
        TT += Time.deltaTime * Speed;

        if (PointFromCenter)
        {
            Vector3 difference = CenterPoint.position - Tf.position;
            rb.rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
    }

    private void FixedUpdate()
    {
        rb.position = new Vector3(CenterPoint.position.x + (XAmp * Mathf.Sin(TT + PhaseOffset) + XOff), CenterPoint.position.y + (YAmp * Mathf.Sin(TT + PhaseOffset) + YOff), 0f);
    }
}
