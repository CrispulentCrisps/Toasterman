using UnityEngine;

public class StemMaster : MonoBehaviour, IPooledObject
{
    protected EnemyScript enemyscript;
    protected ObjectPools objectPooler;
    [SerializeField] private StemFlower[] Flowers;
    [SerializeField] private AnimationCurve MovementCurve;
    [SerializeField] private Transform[] StemPoints;
    private float Osc_t;
    private float Osc_a = 0.1f;
    private float Osc_f = 4f;
    private float CurveTime;
    private bool Died = false;
    [SerializeField] private float StartPos;
    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void FixedUpdate()
    {
        if (CurveTime >= 1f)
        {
            for (int x = 0; x < Flowers.Length; x++)
            {
                Flowers[x].BloomFlower();
            }
            float Osc_p = 0;
            Osc_t += Time.deltaTime;
            for (int i = 0; i < StemPoints.Length; i++)
            {
                StemPoints[i].position = new Vector3(Osc_a * Mathf.Sin(Osc_t * Osc_f + Osc_p) + transform.position.x, StemPoints[i].position.y, StemPoints[i].position.z);
                Osc_p += 0.25f;
            }
        }
        else
        {
            CurveTime += Time.deltaTime / 2;
            transform.position = new Vector3(transform.position.x,StartPos - (MovementCurve.Evaluate(CurveTime) * StartPos*2), transform.position.z);
        }
        int DeadAmount = 0;
        int DetachAmount = 0;
        for (int x = 0; x < Flowers.Length; x++)
        {
            if (Flowers[x].Dead) DeadAmount++;
            if (Flowers[x].Detached) DetachAmount++;
        }

        if (DeadAmount >= Flowers.Length && DetachAmount >= Flowers.Length && !Died)
        {
            for (int x = 0; x < StemPoints.Length; x++)
            {
                if (x == 0)
                {
                    objectPooler.SpawnFromPool("HeadDie", StemPoints[x].position, Quaternion.identity);
                }
                else
                {
                    objectPooler.SpawnFromPool("StemDie", StemPoints[x].position, Quaternion.identity);
                }
                StemPoints[x].gameObject.SetActive(false);
            }
            EnemyScript.EnemyAmount--;
            Died = true;
        }

    }

    public void OnObjectSpawn()
    {
        enemyscript = GameObject.Find("EnemyWaveMaker").GetComponent<EnemyScript>();// gets the scripts for the wave makers
        EnemyScript.EnemyAmount++;
        transform.position = new Vector3(enemyscript.Waves[enemyscript.i].StartYpos, -10f, 0f);
        if (enemyscript.Waves[enemyscript.i].Inverse != 0) StartPos *= enemyscript.Waves[enemyscript.i].Inverse; else StartPos *= 1;
        Died = false;
        for (int x = 0; x < StemPoints.Length; x++)
            StemPoints[x].gameObject.SetActive(true);
    }
}
