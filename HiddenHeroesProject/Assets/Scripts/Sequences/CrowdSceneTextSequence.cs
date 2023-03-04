using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSceneTextSequence : MonoBehaviour
{
    public Animator TextPopupAnimator;
    [TextArea(5, 5)]
    public string[] Passages;
    public StringVariable CurrentDialogueString;
    public float timeBeforeNextPassage = 1.0f;
    public GameEvent RaiseAtEnd;
    public GameEvent RaiseInMiddleA;
    public GameEvent RaiseInMiddleB;
    private bool haveRaisedA = false;
    private bool haveRaisedB = false;


    public void StartSeqeunce()
    {
        StartCoroutine(TextSequenceRoutine());
    }

    private IEnumerator TextSequenceRoutine()
    {
        for (int i = 0; i < Passages.Length; i++)
        {
            TextPopupAnimator.SetTrigger("Reset");
            CurrentDialogueString.Value = Passages[i];
            TextPopupAnimator.SetTrigger("FloatUp");
            yield return new WaitForSeconds(timeBeforeNextPassage);
            if (i == 2 && RaiseInMiddleA != null)
            {
                RaiseInMiddleA.Raise();
            }
            if (i == 4 && RaiseInMiddleB != null)
            {
                RaiseInMiddleB.Raise();
            }
        }

        TextPopupAnimator.SetTrigger("Reset");
        if (RaiseAtEnd != null)
        {
            RaiseAtEnd.Raise();
        }
    }
}
