using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    [SerializeField]
    private AudioClip uiPauseOpen;
    [SerializeField]
    private AudioClip uiPauseClose;
    [SerializeField]
    private AudioClip buttonPush;

    [SerializeField]
    private List<AudioClip> menuMusicList;
    [SerializeField]
    private List<AudioClip> gameMusicList;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    // Since there is no Main menu, it should start as true
    // Also, main menu == pause menu currently...
    private bool isInGame = true;


    public void InitSingleton()
    {
        isInGame = true;
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

    public void UIPauseClose() { PlayClip(uiPauseClose, sfxSource); }

    public void PushButton() { PlayClip(buttonPush, sfxSource); }

    // Plays the given clip in the given source
    public void PlayClip(AudioClip clip, AudioSource src)
    {
        src.clip = clip;
        src.Play();
    }

    public void PlayClip(AudioClip clip, AudioEventType type)
    {
        AudioSource src;

        switch (type)
        {
            case AudioEventType.music:
                src = musicSource;
                break;
            default: 
                src = sfxSource; 
                break;
        }
        src.clip = clip;
        src.Play();
    }
}

