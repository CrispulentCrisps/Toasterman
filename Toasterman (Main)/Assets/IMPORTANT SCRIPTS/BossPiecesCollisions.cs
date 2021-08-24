using UnityEngine;

public class BossPiecesCollisions : MonoBehaviour
{
    ObjectPools objectPooler;

    public TrutleBossAI TurtleBossAI;

    public string SoundName;

    public string[] CollisionNames;

    public double DamageMultiplier;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        for (int i = 0; i < CollisionNames.Length; i++)
        {
            if (coll.gameObject.CompareTag(CollisionNames[i]))
            {
                AudioManager.instance.ChangePitch(SoundName, Random.Range(1f, 0.5f));
                AudioManager.instance.Play(SoundName);
                objectPooler.SpawnFromPool("Spark", new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0f), Quaternion.Euler(0f,-0f,0f));
                TurtleBossAI.Health -= coll.gameObject.GetComponent<DamageScript>().Damage * (float)DamageMultiplier;
            }
        }
    }
}
