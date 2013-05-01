using UnityEngine;
using System.Collections;

public class Drifter : Enemy {

	// Use this for initialization
	void Start () {
        HP = 1;

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
}
