using UnityEngine;
using System.Collections;

public class GameOverGUIText : MonoBehaviour {

    private float deltaCounter;
    private Color red;
    private float lifeTime;

    // Use this for initialization
    void Start() {
        red = Color.red;
        deltaCounter = 0.0f;
        lifeTime = 5.0f;
        gameObject.guiText.material.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        if (deltaCounter <= lifeTime) {
            deltaCounter += Time.deltaTime;
            gameObject.guiText.fontSize += (int)deltaCounter;
            red.a -= deltaCounter / 500;
            gameObject.guiText.material.color = red;
        }
        else {
            Destroy(gameObject);
        }
    }
}
