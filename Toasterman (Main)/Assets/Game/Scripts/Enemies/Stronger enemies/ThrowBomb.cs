using UnityEngine;

public class ThrowBomb : MonoBehaviour, IPooledObject
{
    ObjectPools objectpools;

    public GameObject Flame;

    public Transform tf;
    public Transform Target;
    public Transform FaceTf;
    public Transform BombTf;

    public float SpeedX, SpeedY;
    public float XClamp, YClamp;
    private float XVel, YVel;

    public void OnObjectSpawn()
    {
        XVel = 0;
        YVel = 0;
        tf.position = new Vector3(-17f, -10f, 0f);
    }

    void Start()
    {
        objectpools = ObjectPools.Instance;
        Target = GameObject.Find("Ship").GetComponent<Transform>();
    }

    void Update()
    {
        FaceTf.position = new Vector3(Mathf.Clamp(Target.position.x, tf.position.x - 0.5f, tf.position.x + 0.5f), tf.position.y, tf.position.z);

        if (tf.position.x > Target.position.x)
        {
            XVel -= SpeedX * Time.deltaTime;
        }
        else if (tf.position.x < Target.position.x)
        {
            XVel += SpeedX * Time.deltaTime;
        }
        
        if (tf.position.y < Target.position.y)
        {
            YVel += (SpeedY + 9.81f) * Time.deltaTime;
            Flame.active = true;
        }
        else
        {
            Flame.active = false;
            YVel -= 9.81f * Time.deltaTime;
        }

        if (XVel > XClamp)
        {
            XVel = XClamp;
        }
        else if (XVel < -XClamp)
        {
            XVel = -XClamp;
        }

        if (YVel > YClamp)
        {
            YVel = YClamp;
        }
    }

    public void Fire()
    {
        objectpools.SpawnFromPool("ThrowBomb", BombTf.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        tf.Translate(new Vector3(XVel, YVel) * Time.deltaTime);
    }
}
