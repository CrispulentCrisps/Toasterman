using UnityEngine;

public class BigMushroom : MonoBehaviour
{
    public EnemyScript enemyScript;

    public CameraShake camerashake;

    public Color SwitchColour;

    public Animator anim;

    public SpriteRenderer[] SRends;
    public Sprite[] InfectedSprites;

    public int WaveToAppear;

    void Update()
    {
        if (enemyScript.i == WaveToAppear)
        {
            anim.SetTrigger("Rise");
        }
    }

    public void StartShake()
    {
        StartCoroutine(camerashake.Shake(10f, 0.0075f));
    }

    public void PlayThud()
    {
        AudioManager.instance.Play("Thud2");
        AudioManager.instance.ChangePitch("Thud2", Random.Range(0.9f,1.1f));
    }

    public void SwapBGToInfected()
    {
        for (int i = 0; i < InfectedSprites.Length; i++)
        {
            SRends[i].sprite = InfectedSprites[i];
        }
        SRends[8].color = SwitchColour;
    }
}
