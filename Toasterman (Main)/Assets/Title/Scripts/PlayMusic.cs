using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string MusicName;

    public void MusicPlay()
    {
        AudioManager.instance.Play(MusicName);
    }
}
