using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioClip openItem;
    [SerializeField] private AudioClip thunder;
    [SerializeField] private AudioClip correctRiddle;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Click()
    {
        Play(click);
    }

    public void DoorLocked()
    {
        Play(doorLocked);
    }

    public void OpenItem()
    {
        Play(openItem);
    }

    public void Thunder()
    {
        Play(thunder);
    }

    public void CorrectRiddle()
    {
        Play(correctRiddle);
    }

    private void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
