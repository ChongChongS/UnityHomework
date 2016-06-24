using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {
    string show = null;
    bool bInspectCollision = true;
	// Use this for initialization
	void Start () {
        show = "未发生碰撞";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "Ball")
           show = "进球啦！";
    }

    void OnCollisionExit(Collision collision) { 
        show = "未发生碰撞";
    }

    void OnGUI() {
        GUI.Label(new Rect(100, 0, 300, 40), show);
    }
}
