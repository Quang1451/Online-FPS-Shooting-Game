using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonDontDestroy<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;

    public void Initialize()
    {
    }
    
    public void Play(AudioSource source, AudioClip clip)
    {
        if(source.clip == clip) return;
        source.clip = clip;
        source.Play();
    }

    public void PlayOneShot(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public float GetAudioParameterValue(string group)
    {
        float value;
        if (audioMixer.GetFloat(group, out value))
        {
            return value;
        }
        return 0;
    }
}
