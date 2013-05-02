using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

    public GUISkin mySkin;
    public GUIStyle titleStyle; // Custom style changed in the editor

    void OnGUI() {
        GUI.skin = mySkin;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width-20, Screen.height-20));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Side Scrolling Space Shooter", titleStyle);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Start Game")) {
                    print("Starting game...");
                    Application.LoadLevel("Level");
                }
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Copyright Mike Wells TM 2013");
        GUILayout.EndArea();
    }
}
