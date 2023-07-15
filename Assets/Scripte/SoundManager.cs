using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource cameraAudioSource;
    [SerializeField] private AudioClip[] soundTracks;
    [SerializeField] private float backgroundVolume = 0.2f;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioSource waveAudioSource;
    [SerializeField] private AudioSource laserAudioSource;
    [SerializeField] private AudioSource killAudioSource;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioClip airshipSound;
    [SerializeField] private float fadeTime = 5f;
    private AudioClip actualPlaying;
    private bool fadedIn;


    private void Start()
    {
        cameraAudioSource.volume = backgroundVolume;
        updateSoundtrack();
    }
    private void Update()
    {

        /**
                if (cameraAudioSource.time >= actualPlaying.length)
                {
                    updateSoundtrack();
                }
*/
        if (waveManager.getRemainingWaveCoolDown() == 0
        && waveManager.getWave() != 0
        && fadedIn == false)
        {
            waveAudioSource.Play();
            StartCoroutine(FadeInRoutine(cameraAudioSource));
        }
        else if (waveManager.getRemainingWaveCoolDown() == waveManager.getWaveCoolDown() && fadedIn == true)
        {
            StartCoroutine(FadeOutRoutine(cameraAudioSource));
        }

    }
    private void updateSoundtrack()
    {
        int randomIndex = Random.Range(0, soundTracks.Length);
        actualPlaying = soundTracks[randomIndex];
        cameraAudioSource.clip = actualPlaying;
    }

    IEnumerator FadeInRoutine(AudioSource audioSource)
    {
        float targetVolume = backgroundVolume;
        audioSource.volume = 0f;
        audioSource.Play();
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        fadedIn = true;
    }

    IEnumerator FadeOutRoutine(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        fadedIn = false;
    }

    public void oneShotLaser()
    {
        if (laserAudioSource.isPlaying)
        {
            laserAudioSource.Stop();
        }
        laserAudioSource.PlayOneShot(laserSound);
    }
    public void oneShotKillSound()
    {
        killAudioSource.PlayOneShot(killSound);

    }
    public void oneShotAirshipSound()
    {
        killAudioSource.PlayOneShot(airshipSound);

    }
}
