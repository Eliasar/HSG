using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public GameObject player;
    private Player playerComp;
    public GameObject drifterPrefab;
    public GameObject startText;
    public GameObject gameOverText;
    public bool gameOverBool;
    public bool levelFinished;
    public int currentLevel;

	// Use this for initialization
	void Start () {
        LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (!player.activeSelf && playerComp.livesLeft > 0)
            RespawnPlayer();
        if (playerComp.livesLeft == 0)
            GameOver();

        if (levelFinished) {
            levelFinished = false;

            print("Should be loading level " + currentLevel);
            LoadLevel(++currentLevel);
        }
	}

    void RespawnPlayer() {
        playerComp.respawnTimer += Time.deltaTime;
        if (playerComp.respawnTimer >= playerComp.respawnLimit) {
            playerComp.respawnTimer = 0.0f;
            player.SetActive(true);
            playerComp.init();
        }
    }

    void GameOver() {
        Instantiate(gameOverText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
    }

    void CreateDrifter() {
        Instantiate(drifterPrefab, new Vector3(200, 0, 0), Quaternion.identity);
    }

    void LoadLevel(int level) {
        gameOverBool = false;
        levelFinished = false;
        playerComp = player.GetComponent<Player>();
        startText.guiText.enabled = true;
        startText.guiText.text = "Level " + level;
        Instantiate(startText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);

        float frequency = 1.0f / currentLevel;
        int amount = 5 * currentLevel;

        playerComp.shotInterval = .2f / currentLevel;

        StartCoroutine(LevelCoroutine("drifter", frequency, amount));
    }

    IEnumerator LevelCoroutine(string type, float frequency, int amount) {
        print("Level " + currentLevel + " Coroutine started.");
        print("frequency: " + frequency + "; amount: " + amount);
        for (int i = 0; i < amount; i++) {
            if(type.Equals("drifter")) CreateDrifter();
            yield return new WaitForSeconds(frequency);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) {
            yield return null;
        }

        print("Level " + currentLevel + " Coroutine ended.");
        levelFinished = true;
    }

    
}
