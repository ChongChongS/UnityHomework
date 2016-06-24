using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {
    
    void OnCollisionEnter(Collision col)
    {
        GameObject hit = col.gameObject;
        Health health = hit.GetComponent<Health>();
        AudioSource hitSound = hit.GetComponent<AudioSource>();

        if (health != null)
        {
            health.TakeDamage(10);
            hitSound.Play();
        }
        Destroy(this.gameObject);
    }
}
