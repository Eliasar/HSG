using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public GameObject player;
    public GameObject startText;
    public GameObject gameOverText;
    private bool gameOverBool;

	// Use this for initialization
	void Start () {
        gameOverBool = false;
        startText.guiText.enabled = true;
        Instantiate(startText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
        if (!player.activeSelf && player.GetComponent<Player>().livesLeft > 0)
            RespawnPlayer();
        if (player.GetComponent<Player>().livesLeft == 0) {
            GameOver();
        }
	}

    void RespawnPlayer() {
        player.GetComponent<Player>().respawnTimer += Time.deltaTime;
        if (player.GetComponent<Player>().respawnTimer >= player.GetComponent<Player>().respawnLimit) {
            player.GetComponent<Player>().respawnTimer = 0.0f;
            player.SetActive(true);
            player.GetComponent<Player>().init();
        }
    }

    void GameOver() {
        if (!gameOverBool) {
            Instantiate(gameOverText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            gameOverBool = true;
        }
    }
}
