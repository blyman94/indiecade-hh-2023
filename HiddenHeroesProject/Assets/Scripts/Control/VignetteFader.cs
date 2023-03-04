using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFader : MonoBehaviour
{
    [SerializeField] private Volume volume; // assign the volume asset in the inspector
    [SerializeField] private float fadeDuration = 1f; // duration of the fade in seconds
    [SerializeField] private float targetValue = 0f; // target value of the vignette intensity (0 to 1)

    private Vignette vignette;
    private float initialValue;
    private float currentValue;

    private void Awake()
    {
        volume.profile.TryGet(out vignette);
        initialValue = vignette.intensity.value;
        currentValue = initialValue;
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            currentValue = Mathf.Lerp(initialValue, targetValue, elapsedTime / fadeDuration);
            vignette.intensity.value = currentValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentValue = targetValue;
        vignette.intensity.value = currentValue;
    }
}
