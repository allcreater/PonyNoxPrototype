using UnityEngine;
using System.Collections;

public class TouchSound : MonoBehaviour
{
    private AudioSource m_audioSource;
	// Use this for initialization
	void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
	}
	
    void Update()
    {
        var c = GetComponent<Collider>();
        Debug.DrawLine(c.transform.position, c.attachedRigidbody.position, Color.red);
    }

    void OnCollisionEnter(Collision collision)
    {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}

        if (collision.relativeVelocity.magnitude > 1)
            m_audioSource.Play();
    }


}
