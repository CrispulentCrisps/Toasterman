using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 CenterPos;
    public UnityEngine.Rendering.VolumeProfile profile;
    public UnityEngine.Rendering.Universal.ChromaticAberration myChromaticAberration;

    private void Start()
    {
        profile = GameObject.Find("Global Volume").GetComponent<UnityEngine.Rendering.Volume>().profile;
        profile.TryGet(out myChromaticAberration);
    }
    public IEnumerator AbberationChange(float strength, float increment)
    {
        while (strength > 0f)
        {
            myChromaticAberration.intensity.Override(strength*0.5f);
            strength -= increment * Time.deltaTime;
            yield return null;
        }
    }

    public void SetAbberation(float strength)
    {
        myChromaticAberration.intensity.Override(strength);
    }

    public IEnumerator Shake(float Duration, float Magnitude)
    {
        float Elapsed = 0f;
        while (Elapsed < Duration && Time.timeScale != 0)
        {
            float x = Random.Range(-1f,1f) * Magnitude;
            float y = Random.Range(-1f, 1f) + 1 * Magnitude;
            transform.localPosition = new Vector3(CenterPos.x + x, CenterPos.y + y, CenterPos.z);
            Magnitude -= Mathf.SmoothStep(Magnitude,0f, 3 * Time.deltaTime);
            Elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(CenterPos.x, CenterPos.y, CenterPos.z);
    }

}
