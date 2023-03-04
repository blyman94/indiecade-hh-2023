using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerSceneSequence : MonoBehaviour
{
    public Animator TextPopupAnimator;
    public Animator NextButtonAnimator;
    [TextArea(5,5)]
    public string[] Passages;
    [TextArea(5,5)]
    public string[] PassagesAfter;
    public StringVariable CurrentDialogueString;
    public float startDelayTime = 0.0f;
    public float timeBeforeButton = 2.0f;
    public float timeBeforeNextPassage = 1.0f;
    public bool AwaitingPlayerInput { get; set; } = false;

    private void Start()
    {
        StartCoroutine(DinnerSequenceRoutine());
    }

    private IEnumerator DinnerSequenceRoutine()
    {
        yield return new WaitForSeconds(startDelayTime);

        foreach (string passage in Passages)
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
}
