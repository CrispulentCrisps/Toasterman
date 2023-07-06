using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    Transform Player;
    Transform TargetBox;

    public BoxCollider2D Col;

    public float DefaultCameraSize;
    public float CameraSize;
    public float CameraZoomSpeed;
    public float TargetSize;
    float Step;
    float offset;
    float OgSize;
    
    public AnimationCurve CameraZoomCurve;

    bool TickingUp;
    bool Zooming;

    private void Start()
    {
        VirtualCamera.m_Lens.OrthographicSize = DefaultCameraSize;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Mathf.Clamp(Step, 0f, 1f);
        if (Zooming)
        {
            if (TickingUp)
            {
                Step += Time.deltaTime * CameraZoomSpeed;
            }
            if (Step == 1)
            {
                TickingUp = false;
            }

            VirtualCamera.m_Lens.OrthographicSize = OgSize + (CameraZoomCurve.Evaluate(Step) * offset);
        }
        Col.offset = Dist(transform, Player);
    }
    public void SetTarget(Transform BoxPosition)
    {
        VirtualCamera.Follow = BoxPosition;
        Zooming = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        print("ENTERED");
        if (other.tag == "TargetBox")
        {
            OgSize = VirtualCamera.m_Lens.OrthographicSize;
            Zooming = true;
            TargetBox = other.transform;
            TargetSize = TargetBox.localScale.x / 3.5f;
            SetTarget(TargetBox);
            TickingUp = true;
            offset = TargetSize - OgSize;
            Step = 0;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        print("EXITED");
        if (other.tag == "TargetBox")
        {
            OgSize = VirtualCamera.m_Lens.OrthographicSize;
            Zooming = true;
            SetTarget(Player);
            TickingUp = true;
            TargetSize = DefaultCameraSize;
            offset = TargetSize - OgSize;
            Step = 0;
        }
    }

    Vector2 Dist(Transform pos1, Transform pos2)
    {
        return new Vector2(pos2.position.x-pos1.position.x, pos2.position.y - pos1.position.y);
    }
}
