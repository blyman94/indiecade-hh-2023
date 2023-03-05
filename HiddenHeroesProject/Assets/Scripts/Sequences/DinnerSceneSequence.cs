using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerSceneSequence : MonoBehaviour
{
    public Animator TextPopupAnimator;
    public Animator NextButtonAnimator;
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

    private void Start()
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

        for (int i = 0; i < Passages.Length; i++)
        {
            NextButtonAnimator.Play("NextButton_Idle");
            TextPopupAnimator.SetTrigger("Reset");
            yield return new WaitForSeconds(timeBeforeNextPassage);
            CurrentDialogueString.Value = Passages[i];
            TextPopupAnimator.SetTrigger("FloatUp");
            yield return new WaitForSeconds(timeBeforeButton);
            NextButtonAnimator.Play("NextButton_FloatUp");
            AwaitingPlayerInput = true;
            while (AwaitingPlayerInput)
            {
                yield return null;
            }
        }

        if (IsFirstScene)
        {
            NextButtonAnimator.Play("NextButton_Idle");
            TextPopupAnimator.SetTrigger("Reset");
            truckConvoyAnimator.SetTrigger("MoveLeft");
            foreach (UIImageShake shaker in backgroundShakers)
            {
                shaker.StartShake();
            }
            yield return new WaitForSeconds(cameraShakeTime);

            foreach (string passage in PassagesAfter)
            {
                NextButtonAnimator.Play("NextButton_Idle");
                TextPopupAnimator.SetTrigger("Reset");
                yield return new WaitForSeconds(timeBeforeNextPassage);
                CurrentDialogueString.Value = passage;
                TextPopupAnimator.SetTrigger("FloatUp");
                yield return new WaitForSeconds(timeBeforeButton);
                NextButtonAnimator.Play("NextButton_FloatUp");
                AwaitingPlayerInput = true;
                while (AwaitingPlayerInput)
                {
                    yield return null;
                }
            }
        }

        NextButtonAnimator.Play("NextButton_Idle");
        TextPopupAnimator.SetTrigger("Reset");
        FadeOutSceneEvent?.Raise();
    }
}
