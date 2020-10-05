﻿using UnityEngine;

public class DialogueTranstion : MonoBehaviour
{

    public Dialog dialog;

    public void Start()
    {

        dialog = GameObject.FindGameObjectWithTag("dialog").GetComponent<Dialog>();

    }

    public void StartDialogue()
    {
        FindObjectOfType<AudioManager>().Stop("Level 1");
        StartCoroutine(dialog.BoxIn(1f));
        dialog.index = 1;
    }

}
