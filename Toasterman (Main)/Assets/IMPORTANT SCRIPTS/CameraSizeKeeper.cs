using UnityEngine;

public class CameraSizeKeeper : MonoBehaviour
{

    public bool MaintainWidth = true;

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

            Camera.main.transform.position = new Vector3(CameraPos.x,-1 * (DefaultHeight - Camera.main.orthographicSize),CameraPos.z);

        }
        else
        {



        }

    }
}
