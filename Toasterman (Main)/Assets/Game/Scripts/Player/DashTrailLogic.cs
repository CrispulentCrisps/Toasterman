using UnityEngine;

public class DashTrailLogic : MonoBehaviour, IPooledObject
{
    ObjectPools objectPooler;

    public Transform Ptf;

    private Vector3 PtfScale;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    public void OnObjectSpawn()
    {
        Ptf = GameObject.FindGameObjectWithTag("Player").transform;
        PtfScale = Ptf.localScale;
        gameObject.transform.localScale = PtfScale;
    }

}
