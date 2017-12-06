using UnityEngine;
using UnityEditor;

public class UMW_Helper : EditorWindow
{

    private UMW_Manager Manager;

    void OnEnable()
    {
        Manager = FindObjectOfType<UMW_Manager>();
    }

    void OnGUI()
    {
        if (Manager == null)
            return;

        GUILayout.Label("Go to:");
        for(int i= 0; i < Manager.Windows.Count; i++)
        {
            if (GUILayout.Button(Manager.Windows[i].Name))
            {
                Manager.SetCameraTo(Manager.Windows[i].Position);
            }
        }
    }

    [MenuItem("Window/UMW/Helper")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(UMW_Helper));
    }
}