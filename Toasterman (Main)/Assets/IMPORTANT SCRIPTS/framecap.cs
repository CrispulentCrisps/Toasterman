using UnityEngine;

public class framecap : MonoBehaviour
{

    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

}
