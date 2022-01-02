using UnityEngine;
using TMPro;

public class SoundText : MonoBehaviour
{
    private int index = 0;
    private int PrevSong = 0;

    public AudioManager audioManager;

    public TextMeshProUGUI TextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            audioManager.Stop(audioManager.sounds[PrevSong].name);
            PrevSong = index;
            audioManager.Play(audioManager.sounds[index].name);
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            index--;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            index++;
        }

        if(index <= audioManager.sounds.Length - 1 && index >= 0)
        {
            TextDisplay.text = index + ": " + audioManager.sounds[index].name;
        }
    }
}
