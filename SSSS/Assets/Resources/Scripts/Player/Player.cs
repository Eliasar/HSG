using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    float x;
    float y;

    private int step;
    public int HP;
    public int livesLeft;

    public float shotInterval;
    private float nextShot;

    public float respawnTimer;
    public float respawnLimit;

    private float justSpawnedTimer;
    private float justSpawnedLimit;

    public GameObject laserPrefab;
	public GameObject enemyPrefab;
    public GameObject explosionPrefab;

    float height;
    float width;

    public void init() {
        x = -100;
        y = 0;

        step = 2;
        //shotInterval = 0.2f;
        nextShot = 0.0f;
        HP = 5;
        respawnTimer = 0.0f;
        respawnLimit = 5.0f;
        Camera mainCamera = Camera.mainCamera;
        height = mainCamera.orthographicSize;
        width = height * mainCamera.aspect;

        justSpawnedTimer = 0.0f;
        justSpawnedLimit = 2.0f;

        InvokeRepeating("FlashPlayer", 0, 0.05f);
    }

	// Use this for initialization
	void Start () {
        livesLeft = 2;
        init();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Flash Player if just spawned
        if (justSpawnedTimer < justSpawnedLimit) {
            justSpawnedTimer += Time.deltaTime;
        } else {
            CancelInvoke();
            renderer.enabled = true;
        }

        if (gameObject.activeSelf) {
            if (Input.GetKey("w")) { MoveUp(); }
            else if (Input.GetKey("s")) { MoveDown(); }

            if (Input.GetKey("a")) { MoveLeft(); }
            else if (Input.GetKey("d")) { MoveRight(); }

            if (Input.GetKey("space")) { Fire(); }

            // Actually move the player
            Hashtable ht = new Hashtable();
            ht.Add("x", x);
            ht.Add("y", y);
            ht.Add("time", 0.5f);
            iTween.MoveUpdate(gameObject, ht);
            //y = 0;
            //x = 0;
            nextShot += Time.deltaTime;
        } else {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnLimit) {
                respawnTimer = 0.0f;
                gameObject.SetActive(true);
                init();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) { DEBUG_GenerateEnemy(); }
	}

    void OnCollisionEnter(Collision col) {
        if (enabled) {
            if (col.gameObject.CompareTag("Enemy")) {
                Hit(1);
            }
            else if (col.gameObject.CompareTag("Enemy Laser")) {
                Hit(col.gameObject.GetComponent<DrifterLaser>().power);
            }
        }
    }

    private void MoveUp() {
        if (transform.position.y < height - transform.localScale.y)
            y += step;
        else
            y = height - transform.localScale.y;
    }

    private void MoveDown() {
        if (transform.position.y > -height + transform.localScale.y)
            y -= step;
        else
            y = -height + transform.localScale.y;
    }

    private void MoveLeft() {
        if (transform.position.x > -width + transform.localScale.x)
            x -= step;
        else
            x = -width + transform.localScale.x;
    }

    private void MoveRight() {
        if (transform.position.x < width - transform.localScale.x)
            x += step;
        else
            x = width - transform.localScale.x;
    }

    private void Fire() {
        if (nextShot >= shotInterval) {

            // Create instance
            Instantiate(laserPrefab,
                        transform.position +
                            laserPrefab.transform.localScale,
                        Quaternion.identity);

            // Reset counter
            nextShot = 0.0f;
        }
    }

    private void Hit(int powerOfHit) {
        HP -= powerOfHit;
        if (HP <= 0) {
            livesLeft--;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void FlashPlayer() {
        renderer.enabled = !renderer.enabled;
    }
	
	private void DEBUG_GenerateEnemy() {
		Instantiate(enemyPrefab, new Vector3(200, 0, 0), Quaternion.identity);
	}
}