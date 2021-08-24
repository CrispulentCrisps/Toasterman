using UnityEngine;

public class BigMushroom : MonoBehaviour
{
    ObjectPools objectPooler;

    public EnemyScript enemyScript;
    public Dialog dialog;

    public CameraShake camerashake;

    public Animator anim;

    public SpriteRenderer[] SRends;
    public Sprite[] InfectedSprites;
    public Sprite[] NormalSprites;

    public int WaveToAppear;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }
       
    void Update()
    {
        if (enemyScript.i == WaveToAppear)
        {
            anim.SetTrigger("Rise");
        }
    }

    public void StartShake()
    {
        StartCoroutine(camerashake.Shake(10f, 0.0025f));
    }

    public void PlayThud()
    {
        AudioManager.instance.Play("Thud2");
        AudioManager.instance.ChangePitch("Thud2", Random.Range(0.9f,1.1f));
        objectPooler.SpawnFromPool("SmokeBig", new Vector3(0f,-6f,0f), Quaternion.identity);
    }

    public void PlayFanfare()
    {
        AudioManager.instance.Play("Victory2");
    }
    public void DialogStart()
    {
        dialog.StartCoroutine(dialog.BoxIn(1f));
    }
    public void SwapBGToInfected()
    {
        for (int i = 0; i < InfectedSprites.Length; i++)
        {
            SRends[i].sprite = InfectedSprites[i];
        }
    }
    public void SwapBGToBGNormal()
    {
        for (int i = 0; i < NormalSprites.Length; i++)
        {
            SRends[i].sprite = NormalSprites[i];
        }
    }
}
