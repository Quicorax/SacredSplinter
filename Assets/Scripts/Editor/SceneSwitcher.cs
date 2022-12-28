using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcher : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Quicorax/Scene Switcher")]
    static void Init()
    {
        SceneSwitcher window = (SceneSwitcher)GetWindow(typeof(SceneSwitcher));
        window.Show();
    }

    void OnGUI()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < scenes.Length; i++)
        {
            string path = scenes[i].path;

            if (GUILayout.Button(System.IO.Path.GetFileNameWithoutExtension(path)))
                EditorSceneManager.OpenScene(path);
        }

        EditorGUILayout.EndScrollView();
    }
}
