using UnityEngine;

public class BossUIFunctions : MonoBehaviour
{
    [SerializeField]private Animator Anim;

    public void Opening()
    {
        Anim.SetTrigger("Open");
    }
    public void Closing()
    {
        Anim.SetTrigger("Close");
    }
}
