using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    private string[] Debug;
    private string[] ChangeGraphCode;

    private bool Cheats = false;
    public static bool ChangeGraphics = false;

    private int index;
    private int index2;

    public GameObject SoundTestAcces;

    //Please don't read these codes, or do, I can't really stop you.

    void Start()
    {
        // Code is "baracuda", user needs to input this in the right order
        Debug = new string[] { "b", "a", "r", "a", "c", "u", "d", "a" };
        ChangeGraphCode = new string[] { "l", "e", "t", "t", "h", "e", "s", "t", "u", "p", "i", "d", "i", "t", "y", "r", "u", "n"};
        index = 0;
        index2 = 0;
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
        
        if (Input.anyKeyDown && ChangeGraphics == false)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(ChangeGraphCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index2++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                index2 = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == Debug.Length)
        {
            // Cheat code successfully inputted! Unlock crazy cheat code stuff
            if (!Cheats)
            {
                FindObjectOfType<AudioManager>().Play("Victory2");
                SoundTestAcces.SetActive(true);
                Cheats = true;
                FindObjectOfType<AudioManager>().Stop("Title theme");
            }
        }
        
        if (index2 == ChangeGraphCode.Length)
        {
            // Cheat code successfully inputted! Unlock crazy cheat code stuff
            if (ChangeGraphics == false)
            {
                FindObjectOfType<AudioManager>().Play("BreadzookaBlastTheme");
                ChangeGraphics = true;
            }
        }
    }
}
