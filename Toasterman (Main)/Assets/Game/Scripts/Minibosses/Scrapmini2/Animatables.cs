using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatables : MonoBehaviour
{
    [Range(0, 1)]
    public int BodyType;
    public Transform ParentTrans;
    Transform Target;
    SpriteRenderer Sr;
    public Sprite[] AnimationSprites;

    Vector2 Centerpos;
    
    float P;
    float F;
    int Index;
    public float XDiv;
    bool Flipped = false;
    int State = 0;

    private void Start()
    {
        Centerpos = transform.position;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Sr = gameObject.GetComponent<SpriteRenderer>();
        Index = Mathf.RoundToInt(transform.position.y);
    }

    private void Update()
    {
        F += Time.deltaTime;
        P += Time.deltaTime;
        if (BodyType == 0)
        {
            transform.position = new Vector2(ParentTrans.position.x + Centerpos.x + Mathf.Sin(P + Centerpos.y / 4) * XDiv, ParentTrans.position.y + Centerpos.y + Mathf.Sin(P + Centerpos.y / 8) * 0.25f);
            if (F > .125f)
            {
                if (Index > 2)
                {
                    Index = 0;
                }
                if (Index >= 0)
                {
                    Sr.sprite = AnimationSprites[Index];
                }
                Index++;
                F = 0;
            }
        }
        else if (BodyType == 1 && F > 0.025f)
        {
            transform.position = new Vector2(ParentTrans.position.x + Centerpos.x + Mathf.Sin(P + Centerpos.y / 4) * XDiv, ParentTrans.position.y + Centerpos.y + Mathf.Sin(P + Centerpos.y / 8) * 0.25f);
            if (State == 0)
            {
                if (Target.position.x > transform.position.x)
                {   
                    Index++;
                }
                else if (Target.position.x < transform.position.x)
                {
                    Index--;
                }
                if (Index >= AnimationSprites.Length)
                {
                    Index = AnimationSprites.Length;
                }
                else if (Index < 0)
                {
                    Index = 0;
                }
                else if (Index >= 6)
                {
                    Sr.flipX = true;
                }
                else if (Index < 6)
                {
                    Sr.flipX = false;
                }
            }
            Sr.sprite = AnimationSprites[Index];
            F = 0;
        }
    }
}
