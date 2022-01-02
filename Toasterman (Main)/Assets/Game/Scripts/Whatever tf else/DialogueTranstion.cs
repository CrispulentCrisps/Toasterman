using UnityEngine;

public class DialogueTranstion : MonoBehaviour
{

    public Dialog dialog;

    public void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("dialog").GetComponent<Dialog>();
    }

    public void StartDialogue()
    {
        AudioManager.instance.Stop("Level 1");
        StartCoroutine(dialog.BoxIn(1f));
    }
}
