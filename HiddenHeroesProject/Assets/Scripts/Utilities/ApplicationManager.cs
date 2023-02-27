using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that allows for easy access of Unity's built-in SceneManager, as well 
/// as shutting down the application. An instance of this ScriptableObject can
/// be found for reference in the ScriptableObjects/Utilities directory, or 
/// you should right click in the project view and create an instance there.
/// There only needs to be one instance of the ApplicationManager in the
/// project.
/// </summary>
[CreateAssetMenu]
public class ApplicationManager : ScriptableObject
{
    /// <summary>
    /// WARNING: Do not use this method unless you have a use case for it. Most
    /// of the time you will want to use the LoadSceneSingle methods. Loads a 
    /// scene on top of the existing scene based on it's index number in the 
    /// build settings. Please note, a scene must be added to the build settings 
    /// to be accessible by this method. In that UI it will be assigned an index 
    /// number that can be specified as an argument here. 
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to be loaded.</param>
    public void LoadSceneAdditive(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    /// <summary>
    /// WARNING: Do not use this method unless you have a use case for it. Most
    /// of the time you will want to use the LoadSceneSingle methods. Overload. 
    /// Loads a scene on top of the existing scene based on it's name as a 
    /// string. Please note, a scene must be added to the build settings to be 
    /// accessible by this method.
    /// </summary>
    /// <param name="sceneName">Name of the scene to be loaded.</param>
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Unloads the current scene and loads a scene based on it's index number 
    /// in the build settings. Please note, a scene must be added to the build 
    /// settings to be accessible by this method. In that UI it will be assigned
    /// an index number that can be specified as an argument here.
    /// </summary>
    /// <param name="sceneIndex">Index of the scene to be loaded.</param>
    public void LoadSceneSingle(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    /// <summary>
    /// Overload. Unloads the current scene and loads a scene based on it's
    /// name as a string. Please note, a scene must be added to the build 
    /// settings to be accessible by this method.
    /// </summary>
    /// <param name="sceneName">Name of the scene to be loaded.</param>
    public void LoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Quits the application. Does not work for WebGL.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Reloads the current active scene. Useful for restarts, but keep in mind
    /// all scene-level state will be lost (and all asset-level data will 
    /// persist). Ensure that this is considered in startup functions for this
    /// method to work as intended.
    /// </summary>
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, 
            LoadSceneMode.Single);
    }
}
