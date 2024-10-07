using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioMixer effectMixer;
    
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetEffectVolume (float volume)
    {
        effectMixer.SetFloat("EffectVolume", volume);
    }
}
