using UnityEngine;
using System.Collections;

public class ThirdPersonCam : MonoBehaviour {
    public GameObject follow;
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        targetPosition = follow.transform.position + Vector3.up * distanceUp - follow.transform.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(follow.transform);
    }
}
