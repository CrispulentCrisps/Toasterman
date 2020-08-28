using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMakerCore : MonoBehaviour
{

    private float ExplosionPoint;

    public int start;

    private double XPos;

    ObjectPools objectPooler;

    public CameraShake camerashake;

    public ParticleSystem Boom;

    public Animator CoreAnim;
    public Animator BGAnim;
    public Animator LIGHTAnim;

    public EnemyScript enemyScript;

    void Start()
    {

        objectPooler = ObjectPools.Instance;

    }

    void Update()
    {

        if (enemyScript.i == start)
        {

            CoreAnim.SetTrigger("Enter");
            BGAnim.SetTrigger("AnimA");
        }

    }

    public void StartBGEnd()
    {

        BGAnim.SetTrigger("AnimDone");
        LIGHTAnim.SetTrigger("Normal");

    }

    public void Shatter()
    {

        objectPooler.SpawnFromPool("CoreDeath", new Vector3(0f, -2f, 0f), Quaternion.Euler(-90f, 0f,0f));
        StartCoroutine(camerashake.AbberationChange(1f, 0.05f));
        FindObjectOfType<AudioManager>().Play("BigShatter");
    }

    public void BoomUp()
    {
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)XPos,1f, 0), Quaternion.identity);
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)-XPos, 1f, 0), Quaternion.identity);
        XPos += 0.15;

        FindObjectOfType<AudioManager>().ChangePitch("Shatter", Random.Range(.5f, 2f));
        FindObjectOfType<AudioManager>().Play("Shatter");

    }


}
