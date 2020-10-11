//Source https://www.youtube.com/watch?v=TYNF5PifSmA

using UnityEngine;

public class CameraSizeKeeper : MonoBehaviour
{

    public bool MaintainWidth = true;
    [Range(-1,1)]
    public int adaptPos;

    public float DefaultWidth;
    public float DefaultHeight;

    public Vector3 CameraPos;

    void Start()
    {

        DefaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
        DefaultHeight = Camera.main.orthographicSize;

    }

    void Update()
    {

        if (MaintainWidth)
        {

            Camera.main.orthographicSize = DefaultWidth / Camera.main.aspect;

            Camera.main.transform.position = new Vector3(CameraPos.x,adaptPos*(DefaultHeight - Camera.main.orthographicSize),CameraPos.z);

        }
        else
        {

            Camera.main.transform.position = new Vector3(adaptPos * (DefaultWidth - Camera.main.orthographicSize) * Camera.main.aspect, CameraPos.y, CameraPos.z);

        }

    }
}
