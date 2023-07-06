using UnityEngine;

public class DashTrailLogic : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public string ObjectToLookFor;
    public Transform Ptf;

    private SpriteRenderer Sr;
    private Vector3 PtfScale;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        Sr = GetComponent<SpriteRenderer>();
        Sr.sprite = GameObject.FindGameObjectWithTag(ObjectToLookFor).GetComponent<SpriteRenderer>().sprite;
        Ptf = GameObject.FindGameObjectWithTag(ObjectToLookFor).transform;
        PtfScale = Ptf.localScale;
        gameObject.transform.localScale = PtfScale;
    }
}
