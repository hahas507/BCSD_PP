using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBGM;

    public string[] playingSoundName;

    public Sound[] effectSounds;
    public Sound[] BGMSounds;

    private void Start()
    {
        playingSoundName = new string[effectSounds.Length];
    }

    private void Update()
    {
        audioSourceEffects = GetComponents<AudioSource>();
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int a = 0; a < audioSourceEffects.Length; a++)
                {
                    if (!audioSourceEffects[a].isPlaying)
                    {
                        playingSoundName[a] = effectSounds[i].name;
                        audioSourceEffects[a].clip = effectSounds[i].clip;
                        audioSourceEffects[a].Play();
                        return;
                    }
                }
            }
        }
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < BGMSounds.Length; i++)
        {
            if (_name == BGMSounds[i].name)
            {
                for (int a = 0; a < audioSourceEffects.Length; a++)
                {
                    if (!audioSourceEffects[a].isPlaying)
                    {
                        playingSoundName[a] = BGMSounds[i].name;
                        audioSourceEffects[a].clip = BGMSounds[i].clip;
                        audioSourceEffects[a].Play();
                        return;
                    }
                }
            }
        }
    }

    public void StopAllSound()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playingSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
    }
}