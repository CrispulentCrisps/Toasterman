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

    private bool Ended = false;

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
        if (Ended)
        {
            BGAnim.SetTrigger("AnimDone");
            LIGHTAnim.SetTrigger("Normal");
            AudioManager.instance.Stop("Level 1");
        }
    }

    public void Smoke()
    {
        objectPooler.SpawnFromPool("SmokeBig", new Vector3(0f, -1f, 0f),Quaternion.identity);
    }

    public void Siren()
    {
        AudioManager.instance.Play("Siren");
    }

    public void StartBGEnd()
    {
        Ended = true;
        BGAnim.SetTrigger("AnimDone");
        LIGHTAnim.SetTrigger("Normal");
        AudioManager.instance.Stop("Level 1");
    }

    public void Shatter()
    {
        objectPooler.SpawnFromPool("CoreDeath", new Vector3(0f, -2f, 0f), Quaternion.Euler(-90f, 0f,0f));
        StartCoroutine(camerashake.AbberationChange(1f, 0.075f));
        AudioManager.instance.Play("BigShatter");
    }

    public void Fanfare()
    {
        AudioManager.instance.Play("Victory2");
        PlanetTally.PlanetsDone[0] = true;
        PlanetTally.TimesCompleted[0] += 1;
        if (Shooting.TargetScore > PlanetTally.PlanetScore[0])
        {
            PlanetTally.PlanetScore[0] = Shooting.TargetScore;
        }
        PlanetTally pt = GameObject.FindGameObjectWithTag("TallyCounter").GetComponent<PlanetTally>();
        pt.SaveData();
    }

    public void BoomUp()
    {
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)XPos,1f, 0), Quaternion.identity);
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)-XPos, 1f, 0), Quaternion.identity);
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)XPos * 0.5f, 1f, 0), Quaternion.identity);
        objectPooler.SpawnFromPool("CoreShardBoom", new Vector3((float)-XPos * 0.5f, 1f, 0), Quaternion.identity);
        XPos += 0.75;
    }
}
