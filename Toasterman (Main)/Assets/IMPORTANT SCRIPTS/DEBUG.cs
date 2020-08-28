﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    private string[] Debug;

    private bool Cheats = false;

    private int index;

    public GameObject SoundTestAcces;

    //Please don't read these codes, or do, I can't really stop you.

    void Start()
    {
        // Code is "baracuda", user needs to input this in the right order
        Debug = new string[] { "b", "a", "r", "a", "c", "u", "d", "a" };
        index = 0;
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown && Cheats == false)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(Debug[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == Debug.Length)
        {
            // Cheat code successfully inputted! Unlock crazy cheat code stuff
            if (Cheats == false)
            {
                FindObjectOfType<AudioManager>().Stop("Title theme");
                FindObjectOfType<AudioManager>().Play("Victory2");

                SoundTestAcces.SetActive(true);

                Cheats = true;

            }


        }
    }
}
