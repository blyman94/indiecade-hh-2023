using UnityEngine;
using UnityEngine.UI;

public class UIImageShake : MonoBehaviour
{
    public float shakeIntensity = 0.2f;
    public float shakeDuration = 0.5f;
    public float shakeSpeed = 50f;
    public bool shakeOnStart = false;

    private Vector3 startPosition;
    private float shakeTimeLeft = 0f;

    private void Start()
    {
        startPosition = transform.position;

        if (shakeOnStart)
        {
            StartShake();
        }
    }

    private void Update()
    {
        if (shakeTimeLeft > 0f)
        {
            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float y = Mathf.Cos(Time.time * shakeSpeed) * shakeIntensity;
            transform.position = startPosition + new Vector3(x, y, 0f);

            shakeTimeLeft -= Time.deltaTime;
        }
        else
        {
            transform.position = startPosition;
        }
    }

    public void StartShake()
    {
        shakeTimeLeft = shakeDuration;
    }
}
