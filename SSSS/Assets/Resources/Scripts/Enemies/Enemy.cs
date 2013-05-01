using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int HP;
    public float spawnTimerGrace;

	// Use this for initialization
	void Start () {
        spawnTimerGrace = 0.0f;
	}

    // Update is called once per frame
    void Update() {
        if(spawnTimerGrace <= 1.0f)
            spawnTimerGrace += Time.deltaTime;
    }

    void OnBecameInvisible() {
        // Grace period for spawning
        if (spawnTimerGrace >= 1.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        GameObject colObject = col.gameObject;
        if (colObject.CompareTag("Player Laser")) {
            Hit(colObject.GetComponent<PlayerLaser>().power);
        }
    }

    void Hit(int powerOfHit) {
        HP -= powerOfHit;
        if (HP == 0)
            Destroy(gameObject);
    }
}
