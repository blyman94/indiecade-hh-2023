using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerSceneSequence : MonoBehaviour
{
    public Animator TextPopupAnimator;
    [TextArea(5, 5)]
    public string[] Passages;
    [TextArea(5, 5)]
    public string[] PassagesAfter;
    public StringVariable CurrentDialogueString;
    public float startDelayTime = 0.0f;
    public float timeBeforeButton = 2.0f;
    public float timeBeforeNextPassage = 1.0f;
    public bool AwaitingPlayerInput { get; set; } = false;
    public float cameraShakeTime = 5.0f;
    public UIImageShake[] backgroundShakers;
    public GameEvent FadeOutSceneEvent;
    public bool IsFirstScene = true;
    public Animator truckConvoyAnimator;

    public void StartDinnerSequence()
    {
        StartCoroutine(DinnerSequenceRoutine());
        foreach (UIImageShake shaker in backgroundShakers)
        {
            shaker.shakeDuration = cameraShakeTime;
        }
    }

    private IEnumerator DinnerSequenceRoutine()
    {
        yield return new WaitForSeconds(startDelayTime);
        TextPopupAnimator.SetTrigger("FloatUp");
        
        for (int i = 0; i < Passages.Length; i++)
        {
            yield return new WaitForSeconds(timeBeforeNextPassage);
            CurrentDialogueString.Value = Passages[i];
            yield return new WaitForSeconds(timeBeforeButton);
            AwaitingPlayerInput = true;
            while (AwaitingPlayerInput)
            {
                yield return null;
            }
        }

        if (IsFirstScene)
        {
            TextPopupAnimator.SetTrigger("Reset");
            truckConvoyAnimator.SetTrigger("MoveLeft");
            foreach (UIImageShake shaker in backgroundShakers)
            {
                shaker.StartShake();
            }
            yield return new WaitForSeconds(cameraShakeTime);
            TextPopupAnimator.SetTrigger("FloatUp");

            foreach (string passage in PassagesAfter)
            {
                yield return new WaitForSeconds(timeBeforeNextPassage);
                CurrentDialogueString.Value = passage;
                yield return new WaitForSeconds(timeBeforeButton);
                AwaitingPlayerInput = true;
                while (AwaitingPlayerInput)
                {
                    yield return null;
                }
            }
        }

        TextPopupAnimator.SetTrigger("FloatDown");
        FadeOutSceneEvent?.Raise();
    }
}
