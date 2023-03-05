using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float defaultFadeTime;

    public void FadeOut()
    {
        // Start the coroutine to fade out the volume
        StartCoroutine(FadeOutCoroutine(defaultFadeTime));
    }
    
    public void FadeOut(float fadeTime)
    {
        // Start the coroutine to fade out the volume
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }

    private IEnumerator FadeOutCoroutine(float fadeTime)
    {
        // Get the starting volume
        float startingVolume = audioSource.volume;

        // Fade out the volume over time
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startingVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        // Stop the audio and reset the volume to the starting value
        audioSource.Stop();
        audioSource.volume = startingVolume;
    }
}
