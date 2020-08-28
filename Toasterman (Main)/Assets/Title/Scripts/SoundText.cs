using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundText : MonoBehaviour
{

    private int index = 0;

    public AudioManager audioManager;

    public TextMeshProUGUI TextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        if (index > audioManager.sounds.Length - 1)
        {

            index = audioManager.sounds.Length - 1;

        }
        else if (index < 0)
        {

            index = 0;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

            audioManager.Play(audioManager.sounds[index].name);

        }

        else if (Input.GetKeyDown(KeyCode.X))
        {

            audioManager.Stop(audioManager.sounds[index].name);
            index--;

        }
        else if (Input.GetKeyDown(KeyCode.C))
        {

            audioManager.Stop(audioManager.sounds[index].name);
            index++;

        }

        if(index <= audioManager.sounds.Length - 1 && index >= 0)
        {

            TextDisplay.text = index + ": " + audioManager.sounds[index].name;

        }
        

    }
}
