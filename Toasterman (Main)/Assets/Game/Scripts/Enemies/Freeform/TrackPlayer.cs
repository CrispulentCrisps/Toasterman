using UnityEngine;

public class TrackPlayer : MonoBehaviour, IPooledObject
{
    static ObjectPools objectPooler;
    Transform Target;
    public bool FollowPlayer;
    public bool PointToPlayer;

    public void OnObjectSpawn()
    {

    }

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (PointToPlayer)
        {

        }
    }
}
