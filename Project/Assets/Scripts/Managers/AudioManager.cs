using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    [SerializeField] 
    private AudioSource masterSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource musicSource;
}
