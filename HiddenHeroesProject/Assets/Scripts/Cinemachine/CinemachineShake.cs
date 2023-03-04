using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float shakeFadeInDuration;
    [SerializeField] private float shakeFadeOutDuration;
    [SerializeField] private float shakeAmplitude;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    #region MonoBehavior Methods
    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion

    public void StartShake()
    {
        StartCoroutine(ShakeFadeInRoutine());
    }

    public void StopShake()
    {
        StartCoroutine(ShakeFadeOutRoutine());
    }

    private IEnumerator ShakeFadeInRoutine()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeFadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(0.0f, shakeAmplitude,
                elapsedTime / shakeFadeInDuration);
            yield return null;
        }
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeAmplitude;
    }

    private IEnumerator ShakeFadeOutRoutine()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeFadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(shakeAmplitude, 0.0f,
                elapsedTime / shakeFadeOutDuration);
            yield return null;
        }
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
    }

}
