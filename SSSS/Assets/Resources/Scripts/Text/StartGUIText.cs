using UnityEngine;
using System.Collections;

public class StartGUIText : MonoBehaviour {

    private float deltaCounter;
    private Color blue;
    private float lifeTime;

	// Use this for initialization
	void Start () {
        blue = Color.blue;
        deltaCounter = 0.0f;
        lifeTime = 5.0f;
        gameObject.guiText.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        if (deltaCounter <= lifeTime) {
            deltaCounter += Time.deltaTime;
            gameObject.guiText.fontSize += (int)deltaCounter;
            blue.a -= deltaCounter/500;
            gameObject.guiText.material.color = blue;
        }
        else {
            Destroy(gameObject);
        }
	}
}
