using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour {
	private GameObject camera;
	public Transform thirdPos;
	private float smooth = 1.0f;

	void Start(){
		camera = GameObject.Find ("Main Camera");	
	}

	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

		camera.transform.position = Vector3.Lerp (camera.transform.position, thirdPos.position, Time.deltaTime * smooth);
		camera.transform.rotation = Quaternion.Slerp (camera.transform.rotation, thirdPos.rotation, Time.deltaTime * smooth);
	}
}
