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

    private float invulnerableTimer;
    private float invulnerableLimit;
    public bool vulnerable;

    public GameObject laserPrefab;
    public GameObject explosionPrefab;

    float height;
    float width;

	void Start () {
        livesLeft = 2;
        init();
	}
	
	void LateUpdate () {

        // Check if player is invulnerable
        if (invulnerableTimer < invulnerableLimit) {
            invulnerableTimer += Time.deltaTime;
        } else {
            CancelInvoke();
            renderer.enabled = true;
            vulnerable = true;
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
	}

    public void init() {
        x = -100;
        y = 0;

        step = 2;
        shotInterval = 0.2f;
        nextShot = 0.0f;
        HP = 5;
        respawnTimer = 0.0f;
        respawnLimit = 5.0f;
        Camera mainCamera = Camera.mainCamera;
        height = mainCamera.orthographicSize;
        width = height * mainCamera.aspect;

        invulnerableTimer = 0.0f;
        invulnerableLimit = 2.0f;
        vulnerable = true;
    }

    void OnCollisionEnter(Collision col) {
        if (enabled && vulnerable) {
            if (col.gameObject.CompareTag("Enemy")) {
                Hit(1);
                FlashPlayer();
            }
            else if (col.gameObject.CompareTag("Enemy Laser")) {
                Hit(col.gameObject.GetComponent<DrifterLaser>().power);
                FlashPlayer();
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

            if (livesLeft <= 0) {
                GameObject.FindGameObjectWithTag("GM").GetComponent<Game>().GameOver();
            }
        }
    }

    public void FlashPlayer(float frequency = 0.05f) {
        invulnerableTimer = 0.0f;
        vulnerable = false;
        InvokeRepeating("FlashHelper", 0, frequency);
    }

    private void FlashHelper() {
        renderer.enabled = !renderer.enabled;
    }
}