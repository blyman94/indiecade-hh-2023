using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor script for the CanvasGroupRevealer class. Allows designers to
/// call various functions at editor time.
/// </summary>
[CustomEditor(typeof(CanvasGroupRevealer), editorForChildClasses: true)]
public class CanvasGroupRevealerEditor : Editor
{
    #region Editor Methods
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = !Application.isPlaying;

        CanvasGroupRevealer canvasGroupRevealer = target as CanvasGroupRevealer;
        if (GUILayout.Button("Show"))
        {
            canvasGroupRevealer.Show();
        }
        if (GUILayout.Button("Hide"))
        {
            canvasGroupRevealer.Hide();
        }
    }
    #endregion
}
