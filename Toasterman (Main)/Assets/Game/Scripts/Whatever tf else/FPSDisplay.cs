using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    public bool Show;

    float deltaTime = 0.0f;
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Show = !Show;
        }
    }

    void OnGUI()
    {
        if (Show)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 50);
            style.alignment = TextAnchor.UpperRight;
            style.fontSize = h * 2 / 50;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}
