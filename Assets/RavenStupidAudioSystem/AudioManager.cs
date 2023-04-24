using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioObject;

    [Serializable]
    public class AudioSample
    {
        public AudioClip[] clip;
        [Range(0f,1f)]
        public float volume = 1;

        [Range(-3f, 3f)]
        public float pitch = 1;
        public float pitchVariability = 0;

        public cooldown cd;
    }

    [Header("Player")]
    public AudioSample jump;
    public AudioSample shoot;
    public AudioSample enemyDeath;
    public AudioSample boss;
    public AudioSample bossDeath;
    public AudioSample cathurt;
    public AudioSample money;

    public GameObject Play(string clipName, Vector2 pos, float delay = 0)
    {
        GameObject ao = Instantiate(audioObject, pos, Quaternion.identity);
        AudioSource audioSource = ao.GetComponent<AudioSource>();

        if (audioSource == null) return null;

        switch (clipName.ToLower())
        {
            case "jump":
                CheckAndSetSample(audioSource, jump);
                break;

            case "shoot":
                CheckAndSetSample(audioSource, shoot);
                break;

            case "enemydeath":
                CheckAndSetSample(audioSource, enemyDeath);
                break;

            case "bossdeath":
                CheckAndSetSample(audioSource, bossDeath);
                break;

            case "boss":
                CheckAndSetSample(audioSource, boss);
                break;
            case "cathurt":
                CheckAndSetSample(audioSource, cathurt);
                break;
            case "money":
                CheckAndSetSample(audioSource, money);
                break;
        }

        StartCoroutine(PlaySound(ao, delay));
        Debug.Log("Playing :" + clipName);

        return ao;
    }

    IEnumerator PlaySound(GameObject ao, float delay)
    {
        AudioSource audio = ao.GetComponent<AudioSource>();
        ao.GetComponent<DespawnDelay>().delay = delay;

        yield return new WaitForSeconds(delay); 

        audio.Play();
    }

    bool CanPlaySound(AudioSample audioSample)
    {
        bool condition1 = audioSample.cd.Duration == 0;
        bool condition2 = audioSample.cd.Duration > 0 && audioSample.cd.CurrentProgress != cooldown.Progress.InProgress;

        return condition1 || condition2;
    }

    void CheckAndSetSample(AudioSource audioSource,AudioSample audioSample)
    {
        if (CanPlaySound(audioSample))
        {
            audioSample.cd.StartCooldown();

            int randomClip = 0;//audioSample.clip.Length > 1 ? UnityEngine.Random.Range(0, audioSample.clip.Length) : 0;
            audioSource.clip = audioSample.clip[randomClip];

            audioSource.volume = audioSample.volume;

            float randomPitch = audioSample.pitchVariability == 0 ? 1 : UnityEngine.Random.Range(audioSample.pitch - audioSample.pitchVariability, audioSample.pitch + audioSample.pitchVariability);
            audioSource.pitch = randomPitch;
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource ,float duration, float targetVolume)
    {
        Debug.Log("Fade");

        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            if (audioSource == null) yield break;

            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
