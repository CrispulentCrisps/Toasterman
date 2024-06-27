using UnityEngine;

public class OnOff : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private float TimeBetween;
    [SerializeField] private float Offset;
    private float T;
    private int i;
    private SpriteRenderer Sr;
    private BoxCollider2D Col;

    private void Awake()
    {
        T = Offset;
        Sr = gameObject.GetComponent<SpriteRenderer>();
        Col = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        T += Time.deltaTime;

        if (T > TimeBetween)
        {
            i++;
            Sr.sprite = Sprites[i%2];
            Col.enabled = !Col.enabled;
            T = 0;
        }
    }
}
