using UnityEngine;
using System;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class PlayerEvent : UnityEvent<PlayerBehavior>
{
}


public class TriggerEvent : MonoBehaviour
{
	public PlayerEvent m_OnTriggerEnter;
	public PlayerEvent m_OnTriggerExit;

	void OnTriggerEnter (Collider other)
	{
		var pb = other.GetComponentInChildren<PlayerBehavior> ();
		if (pb)
			m_OnTriggerEnter.Invoke(pb);
	}

	void OnTriggerExit (Collider other)
	{
		var pb = other.GetComponentInChildren<PlayerBehavior> ();
		if (pb)
			m_OnTriggerExit.Invoke(pb);
	}
}
