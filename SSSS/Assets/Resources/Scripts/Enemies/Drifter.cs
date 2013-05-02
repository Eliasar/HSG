using UnityEngine;
using System.Collections;

public class Drifter : Enemy {

    public float shotInterval;
    private float nextShot;

	// Use this for initialization
	void Start () {
        HP = 1;
        nextShot = Random.Range(1.0f, 2.0f);
		int rand = Random.Range(0, 2);
        Hashtable ht = new Hashtable();
        ht.Add("time", 10);
        ht.Add("easetype", iTween.EaseType.linear);

		switch(rand) {
		case 0:
            ht.Add("path", iTweenPath.GetPath("South Snake"));
			break;
        case 1:
            ht.Add("path", iTweenPath.GetPath("North Snake"));
			break;
		}

        iTween.MoveTo(gameObject, ht);
    }

    protected override void Update() {
        base.Update();

        shotInterval += Time.deltaTime;
        if (shotInterval >= nextShot) {
            base.Fire();
            shotInterval = 0.0f;
        }
    }
}
