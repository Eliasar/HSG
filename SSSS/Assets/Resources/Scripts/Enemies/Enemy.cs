using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int HP;
    public float spawnTimerGrace;
    private float gracePeriod;
    public GameObject laserPrefab;
    public GameObject explosionPrefab;

	void Start () {
        spawnTimerGrace = 0.0f;
        gracePeriod = 2.0f;
	}

    protected virtual void Update() {
        if(spawnTimerGrace <= gracePeriod)
            spawnTimerGrace += Time.deltaTime;
    }

    void OnBecameInvisible() {
        if (spawnTimerGrace >= gracePeriod)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player Laser")) {
            Hit(col.gameObject.GetComponent<PlayerLaser>().power);
        }
        if (col.gameObject.CompareTag("Player") && 
            col.gameObject.GetComponent<Player>().vulnerable) {
            Hit(100);
        }
    }

    void Hit(int powerOfHit) {
        HP -= powerOfHit;
        if (HP <= 0) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Fire() {
        Instantiate(laserPrefab,
            transform.position - laserPrefab.transform.localScale,
            Quaternion.identity);
    }
}
