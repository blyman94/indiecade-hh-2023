using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnStart : MonoBehaviour
{
    public UnityEvent OnStartResponse;
    // Start is called before the first frame update
    void Start()
    {
        OnStartResponse?.Invoke();
    }
}
