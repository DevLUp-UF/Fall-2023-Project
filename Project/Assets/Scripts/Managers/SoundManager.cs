using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    [SerializeField]
    private AudioClip uiPauseOpen;

    [SerializeField]
    private List<AudioClip> menuMusicList;
    [SerializeField]
    private List<AudioClip> gameMusicList;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    private bool isInGame = false;


    public void InitSingleton()
    {
        isInGame = false;
    }

    // Make sure appropriate music is playing at all times
    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            if(isInGame)
                PlayClip(gameMusicList[Random.Range(0, gameMusicList.Count)], musicSource);
            else
                PlayClip(menuMusicList[Random.Range(0, menuMusicList.Count)], musicSource);
        }
    }

    public void FlipGameState()
    {
        isInGame = !isInGame;
        if (isInGame)
            PlayClip(gameMusicList[Random.Range(0, gameMusicList.Count)], musicSource);
        else
            PlayClip(menuMusicList[Random.Range(0, menuMusicList.Count)], musicSource);
    }

    public void UIPauseOpen() { PlayClip(uiPauseOpen, sfxSource); }

    // Plays the given clip in the given source
    private void PlayClip(AudioClip clip, AudioSource src)
    {
        src.clip = clip;
        src.Play();
    }
}

