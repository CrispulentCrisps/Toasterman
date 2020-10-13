using UnityEngine;

public class KillObject : MonoBehaviour
{
    public void KillSelf()
    {
        gameObject.SetActive(false);
    }
}
