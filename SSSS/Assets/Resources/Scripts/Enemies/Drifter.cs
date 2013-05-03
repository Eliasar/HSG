using UnityEngine;
using System.Collections;

public class Drifter : Enemy {

    public float shotInterval;
    private float nextShot;

    public int rand;
    public Hashtable ht;

	// Use this for initialization
	void Start () {
        HP = 1;
        nextShot = Random.Range(1.0f, 2.0f);
		rand = Random.Range(0, 2);
        ht = new Hashtable();
        ht.Add("time", 7);
        ht.Add("easetype", iTween.EaseType.linear);
        ht.Add("onComplete", "Reset");
        ht.Add("onCompleteTarget", gameObject);

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

    protected override void OnBecameInvisible()
    {
        //base.OnBecameInvisible();
        /*if (spawnTimerGrace >= gracePeriod) {
            //iTween.PutOnPath(gameObject, iTweenPath.GetPath("North Snake"), 0.0f);
            //iTween.MoveTo(gameObject, ht);
        }*/
    }

    void Reset() {
        transform.position = new Vector3(200, 80, 0);
        iTween.MoveTo(gameObject, ht);
    }
}
