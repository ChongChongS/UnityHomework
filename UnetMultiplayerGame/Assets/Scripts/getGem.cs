using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class getGem : NetworkBehaviour
{
    void OnCollisionEnter(Collision col)
    {
		GameObject hit = col.gameObject;
		Health health = hit.GetComponent<Health>();

		if (health != null)
		{
			health.recover (10);
			Debug.Log (health.currentHealth);
		}
        Destroy(this.gameObject);
    }
}
