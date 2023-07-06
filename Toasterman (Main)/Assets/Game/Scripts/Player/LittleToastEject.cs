using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleToastEject : MonoBehaviour
{
    public GameObject ToastyBoi;
    Transform ToastTf;
    Rigidbody2D rb;
    private void Start()
    {
        ToastTf = transform;
        rb = GetComponent<Rigidbody2D>();
        ToastTf.position = transform.parent.position;
    }

    private void Update()
    {
        ToastTf.Rotate(0f, 0f, 360f * Time.deltaTime);
        if (transform.position.y <= -10f)
        {
            gameObject.active = false;
        }
    }

}
