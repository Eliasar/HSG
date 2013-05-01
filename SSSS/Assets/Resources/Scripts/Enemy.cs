using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private float delta;

	// Use this for initialization
	void Start () {
		int rand = Random.Range(0, 2);
		
		switch(rand) {
		case 0:
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("South Snake"),
											      "time", 10,
											      "easetype", iTween.EaseType.linear));
			break;
		case 1:
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("North Snake"),
											      "time", 10,
											      "easetype", iTween.EaseType.linear));
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
        delta += Time.deltaTime;
	}
	
	void OnBecameInvisible() {
        // Grace period for spawning
        if(delta >= 1.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player Laser")) {
            Destroy(gameObject);
        }
    }
}
