using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    Transform Target;
    public bool FollowPlayer;
    public bool PointToPlayer;
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
