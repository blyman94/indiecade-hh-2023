using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndRaiseEvent : MonoBehaviour
{
    public float waitDuration = 1.0f;

    public GameEvent gameEvent;

    public void Execute()
    {
        StartCoroutine(WaitAndRaiseRoutine());
    }

    private IEnumerator WaitAndRaiseRoutine()
    {
        yield return new WaitForSeconds(waitDuration);
        gameEvent.Raise();
    }
}
