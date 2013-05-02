using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

    public GUISkin mySkin;
    //public GUIStyle titleStyle; // Custom style changed in the editor
    public Player player;
    public Game game;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnGUI() {
        GUI.skin = mySkin;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));
        GUILayout.Label("Level: " + game.currentLevel);
        GUILayout.Label("Lives: " + player.livesLeft);
        GUILayout.Label("HP: " + player.HP);
        GUILayout.EndArea();
    }
}
