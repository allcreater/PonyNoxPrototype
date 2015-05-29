using UnityEngine;
using System.Collections;

public class SoundOnContact : MonoBehaviour
{
	private AudioSource m_as;

	private Vector3 m_lastPosition;
	private Vector3 m_acceleration;

	void Start ()
	{
		m_as = GetComponent<AudioSource> ();
		m_lastPosition = transform.position;
	}

	void FixedUpdate()
	{
		var position = transform.position;
		m_acceleration = (m_lastPosition - position) / Time.fixedDeltaTime;
	}

	void OnTriggerEnter (Collider other)
	{
		if (true) {
			Debug.Log (m_acceleration.magnitude);
			m_as.Play ();
		}
	}
}
