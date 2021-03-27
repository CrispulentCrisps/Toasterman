using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Audio stop by https://answers.unity.com/questions/194110/how-to-stop-all-audio.html

public class SceneChange : MonoBehaviour
{

    public Animator transition;
    public bool ConditionMet = false;
    public bool FadeInMusic;
    public float Time;

    public string MusicName;

    public int SceneNumber;

    private AudioSource[] allAudioSources;

    void Start()
    {

        if (MusicName != "")
        {
            FindObjectOfType<AudioManager>().Play(MusicName);
            FindObjectOfType<AudioManager>().SetVolume(MusicName, 0f);
            if (FadeInMusic == true)
            {
                StartCoroutine(AudioManager.instance.FadeAudio(MusicName, -0.5f));
            }
            else
            {
                AudioManager.instance.SetVolume(MusicName, 1f);
            }
        }

    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public void ChangeScene()
    {
        if (ConditionMet == true)
        {
            StartCoroutine(LoadLevel(SceneNumber));
        }
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(Time);
        StopAllAudio();
        SceneManager.LoadScene(LevelIndex);
    }

}
