using UnityEngine;
using UnityEngine.SceneManagement;

public class SurviveOneSceneTransition : MonoBehaviour
{
    int scenesSurvived = 0;

    private void Awake()
    {
        // Don't destroy this object when the scene changes
        DontDestroyOnLoad(gameObject);
        scenesSurvived = 0;
    }

    private void OnEnable()
    {
        // Subscribe to the scene loading event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loading event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scenesSurvived++;
        if (scenesSurvived > 2)
        {
            // Destroy this object after the first scene transition
            Destroy(gameObject);
        }
    }
}
