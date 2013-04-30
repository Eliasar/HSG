using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

    public int speed;

	void Start () {
	}
	
	void Update () {
        iTween.MoveBy(gameObject, new Vector3(speed, 0, 0), 0);
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter() {
        Destroy(gameObject);
    }
}
