using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image image;
    public float fadeInTime = 1f;
    public float fadeOutTime = 1f;
    public float flickerInterval = 0.1f;
    public UnityEvent RaiseAtEnd;

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void StartFlickerEffect(int numFlickers)
    {
        StartCoroutine(Flicker(numFlickers));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);
            color.a = alpha;
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
        if (RaiseAtEnd != null)
        {
            RaiseAtEnd.Invoke();
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);
            color.a = alpha;
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }

    private IEnumerator Flicker(int numFlickers)
    {
        float initialAlpha = image.color.a;
        for (int i = 0; i < numFlickers; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            yield return new WaitForSeconds(flickerInterval);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            yield return new WaitForSeconds(flickerInterval);
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        if (RaiseAtEnd != null)
        {
            RaiseAtEnd.Invoke();
        }
    }
}
