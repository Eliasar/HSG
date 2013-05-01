using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float x;
    public float y;
    private Vector3 playerPos;

    public int step;
    public float shotInterval;
    private float nextShot;

    public GameObject laserPrefab;
	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
        x = -100;
        y = 0;

        step = 5;
        nextShot = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w")) {
            MoveUp();
        }
        else if (Input.GetKey("s")) {
            MoveDown();
        }

        if (Input.GetKey("a")) {
            MoveLeft();
        }
        else if (Input.GetKey("d")) {
            MoveRight();
        }

        if (Input.GetKey("space")) {
            Fire();
        }
		
		if(Input.GetKey (KeyCode.KeypadPlus)) {
			DEBUG_GenerateEnemy();
		}

        // Actually move the player
        iTween.MoveBy(gameObject, new Vector3(x, y, 0), 0);
        y = 0;
        x = 0;
        nextShot += Time.deltaTime;
	}

    private void MoveUp() {
        y += step;
    }

    private void MoveDown() {
        y -= step;
    }

    private void MoveLeft() {
        x -= step;
    }

    private void MoveRight() {
        x += step;
    }

    private void Fire() {
        if (nextShot >= shotInterval) {

            // Recalculate laser position
            CalculatePlayerPosition();

            // Create instance
            Instantiate(laserPrefab, playerPos, Quaternion.identity);

            // Reset counter
            nextShot = 0.0f;
        }
    }

    private void CalculatePlayerPosition() {
        playerPos = new Vector3(transform.position.x + transform.localScale.x / 2 +
                                laserPrefab.transform.localScale.x / 2,
                                transform.position.y,
                                0);
    }
	
	private void DEBUG_GenerateEnemy() {
		Instantiate(enemyPrefab, new Vector3(200, 0, 0), Quaternion.identity);
	}
}