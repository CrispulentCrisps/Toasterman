using UnityEngine;

public class DialogueTranstion : MonoBehaviour
{

    public Dialog dialog;
    public string LevelTheme;

    public void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("dialog").GetComponent<Dialog>();
    }

    public void StartDialogue()
    {
        AudioManager.instance.Stop(LevelTheme);
        StartCoroutine(dialog.BoxIn(1f));
    }
}
