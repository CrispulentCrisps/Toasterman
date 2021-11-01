using UnityEngine;

public class WaitThenDeactivate : MonoBehaviour, IPooledObject
{
    private float T = 0f;
    public float Max;
    private bool Spawned = false;
    ObjectPools objectPooler;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
        T = 0;
    }

    public void OnObjectSpawn()
    {
        T = 0;
        Spawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawned)
        {
            T += Time.deltaTime;
            print(T);
        }
        if (T >= Max && Spawned == true)
        {
            gameObject.SetActive(false);
            Spawned = false;
            T = 0;
        }
    }
}
