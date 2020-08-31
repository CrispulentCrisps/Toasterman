using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Chromatic abberation from https://www.reddit.com/r/Unity3D/comments/hbch73/how_do_you_change_chromatic_aberration_through/

public class CameraShake : MonoBehaviour
{


    public IEnumerator AbberationChange(float strength, float increment)
    {

        UnityEngine.Rendering.VolumeProfile profile = GameObject.Find("Global Volume").GetComponent<UnityEngine.Rendering.Volume>().profile;
        UnityEngine.Rendering.Universal.ChromaticAberration myChromaticAberration;
        profile.TryGet(out myChromaticAberration);
        while (strength > 0f)
        {

            myChromaticAberration.intensity.Override(strength);

            strength -= increment * Time.deltaTime;

            yield return null;

        }

    }

    public void SetAbberation(float strength)
    {

        UnityEngine.Rendering.VolumeProfile profile = GameObject.Find("Global Volume").GetComponent<UnityEngine.Rendering.Volume>().profile;
        UnityEngine.Rendering.Universal.ChromaticAberration myChromaticAberration;
        profile.TryGet(out myChromaticAberration);

        myChromaticAberration.intensity.Override(strength);


    }

    public IEnumerator Shake(float Duration, float Magnitude)
    {

        float Elapsed = 0f;

        while (Elapsed < Duration)
        {

            float x = Random.Range(-1f,1f) * Magnitude;
            float y = Random.Range(-1f, 1f) + 1 * Magnitude;

            transform.localPosition = new Vector3(x, y, -10);

            Elapsed += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = new Vector3(0,1,-10);

    }

}
