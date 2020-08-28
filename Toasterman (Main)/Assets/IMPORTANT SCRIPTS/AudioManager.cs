using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixerGroup audioMixer;

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {

            instance = this;

        }
        else
        {

            Destroy(gameObject);
            return;

        }

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer;

        }

    }

    ///+==========================================================================================+
    ///|                                       Functions                                          |
    ///+==========================================================================================+

    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void ChangePitch(string name, float Pitch)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = Pitch;

    }

    public IEnumerator FadeAudio(string name, float Increment)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        while (s.source.volume > 0f || s.source.volume < 1f)
        {

            s.source.volume -= Increment * Time.deltaTime;

            yield return null;

        }

        s.source.Stop();

    }

}
