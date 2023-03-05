using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSequencer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip oneTimeSong;
    public AudioClip repeatSong;
    public float timeOffset = -0.1f;

    public void Start()
    {
        audioSource.loop = false;
        audioSource.clip = oneTimeSong;
        audioSource.Play();
        StartCoroutine(WaitForSongToFinish());
    }

    public IEnumerator WaitForSongToFinish()
    {
        yield return new WaitForSeconds(oneTimeSong.length + timeOffset);
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = repeatSong;
        audioSource.Play();
    }
}
