using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // public string[] VolumeParameters => volumeParameters;
    // public AudioMixer AudioMixer => audioMixer;

    [SerializeField] private AudioClip Click;
    
    // [SerializeField] private AudioClip[] Music;

    // Generic audio sources
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioSource musicPlayer;
    
    // AudioRandomContainer sources for randomized sounds
    [SerializeField] private AudioSource whooshPlayer;
    [SerializeField] private AudioSource obstacleHitPlayer;

    // [SerializeField] private AudioMixer audioMixer;

    // [SerializeField] private string[] volumeParameters;

    // private Queue<AudioClip> MusicQueue;
    // public bool PlayMusic { get; set; }

    // private IEnumerator musicControlRoutine; 

    public enum SoundEventType
    {
        Click,
        Whoosh,
        ObstacleHit,
    }

    void Awake()
    {
        // Singleton setup;
        if (Services.instance.Get<AudioManager>() != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        Services.instance.Set(this);

        // ShuffleMusicQueue();
        // PlayMusic = true;

        /*
        foreach (string volumeParameter in volumeParameters)
        {
            float volume; ;
            if (PlayerPrefs.HasKey(volumeParameter))
            {
                volume = PlayerPrefs.GetFloat(volumeParameter);
                audioMixer.SetFloat(volumeParameter, volume);
            }
            else
            {
                audioMixer.GetFloat(volumeParameter, out volume);
                PlayerPrefs.SetFloat(volumeParameter, volume);
            }
        }
        */
    }

    private void Start()
    {
        EventBus.Events
            .Where(eventRecord => eventRecord.Kind == EventBus.EventType.SoundEvent)
            .Subscribe(eventRecord =>
            {
                Debug.Log($"Event: {eventRecord}");
                var soundEventType = eventRecord switch
                {
                    EventBus.SoundEvent soundEvent => soundEvent.SoundEventType,
                    _ => throw new ArgumentException($"Unexpected event record kind: {eventRecord}"),
                };
                
                PlaySound(soundEventType);
            })
            .AddTo(this);
    }

    /*
    private void Update() {
        if (PlayMusic && !musicPlayer.isPlaying) {
            try {
                var newClip = MusicQueue.Dequeue();
                PlayMusicClip(newClip);
            } catch (InvalidOperationException) {
                ShuffleMusicQueue();
            }
        }
        if(musicControlRoutine != null)
        {
            if(!musicControlRoutine.MoveNext())
            {
                musicControlRoutine = null;
            }
        }
    }
    */



    // void ShuffleMusicQueue() {
    //     var musicListShuffled = Music.ToList();
    //     musicListShuffled.Shuffle();
    //     MusicQueue = new Queue<AudioClip>(musicListShuffled);
    // }
    
    public void PlaySound(SoundEventType soundEventType)
    {
        switch (soundEventType)
        {
            case SoundEventType.Click:
            {
                PlaySFXClip(GetAudioClip(soundEventType));
                return;
            }
            case SoundEventType.Whoosh:
            {
                whooshPlayer.Play();
                return;
            }
            case SoundEventType.ObstacleHit:
            {
                obstacleHitPlayer.Play();
                return;
            }
            default:
                throw new ArgumentException($"Unexpected {nameof(soundEventType)} {soundEventType}", nameof(soundEventType));
        }
    }

    private AudioClip GetAudioClip(SoundEventType soundEventType)
    {
        return soundEventType switch
        {
            SoundEventType.Click => Click,
            _ => throw new ArgumentOutOfRangeException(nameof(soundEventType), soundEventType, null)
        };
    }

    private void PlaySFXClip(AudioClip clip)
    {
        Debug.LogWarning($"AudioManager#PlayClip:{clip}");
        sfxPlayer.PlayOneShot(clip);
    }
    
    private void PlayMusicClip(AudioClip clip)
    {
        musicPlayer.PlayOneShot(clip);
    }
}