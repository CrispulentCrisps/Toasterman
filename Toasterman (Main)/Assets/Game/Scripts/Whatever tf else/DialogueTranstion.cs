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

        StartCoroutine(dialog.BoxIn(1f));
        dialog.index = 1;
    }

}
