using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _audioSource;
        
    private void Awake()
    {
        Instance = this;
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays an AudioClip, and scales the AudioSource volume by volumeScale.
    /// </summary>
    /// <param name="audioClip">The clip being played.</param>
    /// <param name="volumeScale">The scale of the volume (0-1).</param>
    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1)
    {
        _audioSource.PlayOneShot(audioClip, volumeScale);
    }

    /// <summary>
    /// Plays an AudioClip, and scales the AudioSource volume by volumeScale. Randomizes pitch on each play.
    /// </summary>
    /// <param name="audioClip">The clip being played.</param>
    /// <param name="minPitch">Min pitch of the clip being played.</param>
    /// <param name="maxPitch">Max pitch of the clip being played.</param>
    /// <param name="volumeScale">The scale of the volume (0 - 1).</param>
    public void PlayOneShot(AudioClip audioClip, float minPitch, float maxPitch, float volumeScale = 1f)
    {
        if (minPitch > maxPitch)
        {
            throw new ArgumentException("minPitch cannot be higher than maxPitch!");
        }
        
        //Randomize pitch before each play
        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        PlayOneShot(audioClip, volumeScale);
    }
}