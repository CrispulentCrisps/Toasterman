using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRot : MonoBehaviour
{
    public Transform RefTrans;
    public Vector2 ZClamp;

    private void FixedUpdate()
    {
        float Rot = Mathf.Clamp(transform.rotation.z + RefTrans.rotation.z, ZClamp.x, ZClamp.y);
        transform.eulerAngles = new Vector3(0f, 0f, Rot);
    }
}
