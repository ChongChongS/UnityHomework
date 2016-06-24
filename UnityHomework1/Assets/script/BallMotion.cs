using UnityEngine;
using System.Collections;

public class BallMotion : MonoBehaviour {
    bool bInspectCollision = true;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (bInspectCollision) 
        {
            if (collision.gameObject.name == "widget")
            {
                Vector3 force = this.transform.position - collision.gameObject.transform.position;
                rb.AddForceAtPosition(force * 10, this.transform.position, ForceMode.Impulse);
            }
        }
    }
}
