using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private SpriteRenderer SRender;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start()
    {
        SRender = gameObject.GetComponent<SpriteRenderer>();
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        objectWidth = SRender.bounds.extents.x; //extents = size of width / 2
        objectHeight = SRender.bounds.extents.y; //extents = size of height / 2
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
